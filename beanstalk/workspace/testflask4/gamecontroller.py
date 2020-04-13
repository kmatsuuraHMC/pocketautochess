from player import Player
from minions.minion import *
import json

class GameController:
    """
    ゲームの進行を制御するオブジェクト
    """
    def __init__(self):
        self.state = 0
        self.win = 0
        self.game = GameObject()
        self.time = 0    
        self.log = []
        self.player1 = Player("player1")
        self.player2 = Player("player2")
        
    def deploy_minions(self, data):
        """
        jsonを読み込んでself.gameObjectの中にminionを配置

        Attribute
        -----------------
        data: dict
            読み込んできたデータ、jsonfileの読み込みはapplication.pyの方でする
        Return
        -----------------
        minions: list<Minion>
        """
        minons = []
        key = data["key"]
        if key == self.player1.key:
            team = 1
        elif key == self.player2.key:
            team = 2
        if data["type"] != "deploy":
            return "jsonfileのtypeがdeployでありません"
        for i in range(10):
            minion_data = data["deploy"]["yourTeam"][i]
            number = minion_data["number"]
            race = minion_data["race"]
            x = minion_data["x"]
            y = minion_data["y"]
            self.game.choose_deploy_minion(race, number, team, x, y)
        

    def send_minions(self):
        """
        クライアントにminionsの位置情報を送る。（未実装）
        self.game.player1_alive_minion_listのデータを10個送る
        """
        pass

    def execute_game(self):
        """
        ゲームの実行
        """
        while(self.game.win == 0):
            self.log.append((self.time, self.game))
            self.time += 1
            self.game.time_evolve()
        
        self.state = 9


class GameObject:
    """
    ゲームの状態空間（スナップショット）
    """

    def __init__(self):
        self.player1_alive_minions_list = []
        self.player2_alive_minions_list = []
        self.player1_dead_minions_list = []
        self.player2_dead_minions_list = []
        self.win = 0

    def choose_deploy_minion(self, race, id, team, positionx, positiony):
        """
        どのミニオンを配置するか指定
        """
        if race == "Goburui":
            minion = Gobrui(id)
        elif race == "Gagoiru":
            minion = Gagoiru(id)
        elif race == "Maruta":
            minion = Maruta(id)
        self.deploy_minion(minion, team, positionx, positiony)

    def deploy_minion(self, minion, team, positionx, positiony):
        """
        コマの配置をする。
        Attributes
        ----------------
        minon: Minion
        team: int
        """
        minion.team = team
        minion.positiony = positiony
        if team == 1:
            minion.positionx = positionx
            self.player1_alive_minions_list.append(minion)
        elif team == 2:
            minion.positionx = -1 * positionx
            self.player2_alive_minions_list.append(minion)

    def minion_to_json():
        """
        deployフェイズでクライアントに送るべき情報をjson形式に変換する。
        """
        pass

    def time_evolve(self):
        """
        ターンを進行する処理
        """
        self.movephase()
        self.battlephase()
        self.endphase()
        self.write_log()

    def movephase(self):
        """
        minonの移動をするフェイズ
        """
        for player1minion in self.player1_alive_minions_list:
            if player1minion.action == "move":
                self.move(player1minion, self.player2_alive_minions_list)
            
        for player2minion in self.player2_alive_minions_list:
            if player2minion.action == "move":
                self.move(player2minion, self.player1_alive_minions_list)

            
    
    def battlephase(self, parameter_list):
        """
        minion全員分の攻撃に関する一連の処理
        """
        for player1minion in self.player1_alive_minions_list:
            if player1minion.action == "attack":
                self.attack(player1minion, self.player2_alive_minions_list)
            
        for player2minion in self.player2_alive_minions_list:
            if player2minion.action == "attack":
                self.attack(player2minion, self.player1_alive_minions_list)

    def endphase(self):
        """
        ターン終了時にする処理。履歴をログに書く。hp0以下のミニオンの除去、勝敗条件の確認を行う。
        """
        #hp0以下のミニオンの削除
        temp = self.player1_alive_minions_list.copy()
        for minion in temp:
            if minion.hp <= 0:
                self.player1_alive_minions_list.remove(minion)
                self.player1_dead_minions_list.append(minion)

        temp = self.player2_alive_minions_list.copy()
        for minion in temp:
            if minion.hp <= 0:
                self.player2_alive_minions_list.remove(minion)
                self.player2_dead_minions_list.append(minion)

        #勝敗条件の確認
        if not self.player1_alive_minions_list:
            if not self.player2_alive_minions_list:
                # 引き分け
                self.win = 3
            elif self.player2_alive_minions_list:
                # player2の勝利
                self.win = 2
        
        elif not self.player2_alive_minions_list:
            # player1の勝利
            self.win = 1

    def write_log(self):
        """
        gameの状態のうち、クライアントに送るべき情報をself.logに保存（未実装）
        """

        pass

    def attack(self, attacker, target_minion_list):
        """
        攻撃対象リストの中から攻撃対象を選んで攻撃

        Parameters
        --------------
        attacker:Minion
            攻撃するミニオン
        target_minion_list:list<Minion>
            攻撃対象のミニオンの候補
        """
        attacker.choose_target(target_minion_list)
        attacker.attack()

    

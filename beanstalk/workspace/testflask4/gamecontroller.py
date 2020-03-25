from player import Player
from minions.minion import *

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

    def deploy_minions(self):
        """
        jsonを読み込んでminionsを配置(未実装)
        """
        pass

    def send_minions(self):
        """
        クライアントにminionsのは位置情報を送る。（未実装）
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
        self.player1 = Player("player1")
        self.player2 = Player("player2")
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
        self.deploy(minion, team, positionx, positiony)

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

    

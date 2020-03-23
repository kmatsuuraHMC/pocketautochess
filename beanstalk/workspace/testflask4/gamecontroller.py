from player import Player
class GameController:
    """
    ゲームの進行を制御するオブジェクト
    """
    def __init__(self):
        self.state = 0
        self.win = 0
        self.game = GameObject()    


    def execute_game(game):


class GameObject:
    """
    ゲームの状態空間（スナップショット）
    """

    def __init__(self):
        self.player1 = Player()
        self.player2 = Player()
        self.player1_alive_minions_list = []
        self.player2_alive_minions_list = []
        self.player1_dead_minions_list = []
        self.player2_dead_minions_list = []
        self.win = 0

    
    def set_player_name(self, player_nunmber, player_name):
        """
        set player name
        """
        if player_nunmber == 1:
            self.player1.name = player_name
        elif player_nunmber == 2:
            self.player2.name = player_name
    
    def deploy_minion(self, name):
        """
        コマの配置をする。
        """
        return
    
    def attack(self, attacker, target):
        """
        攻撃

        Parameters
        --------------
        attacker:Minion
            攻撃するミニオン
        target:Minion
            攻撃対象のミニオン
        """
        target.hp -= attacker.atk
    
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

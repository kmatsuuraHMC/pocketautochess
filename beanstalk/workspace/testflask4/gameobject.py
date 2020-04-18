from minions.minion import Gagoiru, Gobrui, Maruta


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

    def time_evolve(self):
        """
        ターンを進行する処理
        """
        self.standbyphase()
        self.movephase()
        self.battlephase()
        self.endphase()
        self.write_log()

    def standbyphase(self):
        """
        targetの選択、actionの設定をする。
        """
        for player1minion in self.player1_alive_minions_list:
            player1minion.choose_target(self.player2_alive_minions_list)
            player1minion.set_action()

        for player2minion in self.player2_alive_minions_list:
            player2minion.choose_target(self.player1_alive_minions_list)
            player2minion.set_action()

    def movephase(self):
        """
        minonの移動をするフェイズ
        """
        for player1minion in self.player1_alive_minions_list:
            if player1minion.action == "move":
                player1minion.move()

        for player2minion in self.player2_alive_minions_list:
            if player2minion.action == "move":
                player2minion.move()

    def battlephase(self):
        """
        minion全員分の攻撃に関する一連の処理
        """
        for player1minion in self.player1_alive_minions_list:
            if player1minion.action == "attack":
                player1minion.attack()

        for player2minion in self.player2_alive_minions_list:
            if player2minion.action == "attack":
                player2minion.attack()

    def endphase(self):
        """
        ターン終了時にする処理。履歴をログに書く。hp0以下のミニオンの除去、勝敗条件の確認を行う。
        """
        # hp0以下のミニオンの削除
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

        # 勝敗条件の確認
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

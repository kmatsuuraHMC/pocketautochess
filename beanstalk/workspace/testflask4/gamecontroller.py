from player import Player
from gameobject import GameObject


class GameController:
    """
    ゲームの進行を制御するオブジェクト
    時間変化に対してイミュータブルな情報は基本こっちに持たせる。
    """

    def __init__(self):
        self.state = 0
        self.win = 0
        self.game = GameObject()
        self.time = 0
        self.log = []
        self.player1 = Player("player1")
        self.player2 = Player("player2")
        self.player1_minion_infomation = []
        self.player2_minion_infomation = []
        self.player1_deploy = 0
        self.player2_deploy = 0

    def deploy_minions(self, data):
        """
        jsonを読み込んでself.gameObjectの中にminionを配置,minion_infomationにminionの情報を入れる。

        Attribute
        -----------------
        data: dict
            読み込んできたデータ、jsonfileの読み込みはapplication.pyの方でする
        Return
        -----------------
        minions: list<Minion>
        """
        key = data["key"]
        if key == self.player1.key:
            team = 1
        elif key == self.player2.key:
            team = 2
        if data["type"] != "deploy":
            return "jsonfileのtypeがdeployでありません"
        for i in range(10):
            minion_data = data["deploy"]["yourTeam"][i]
            number = int(minion_data["number"])
            race = minion_data["race"]
            x = float(minion_data["x"])
            y = float(minion_data["y"])
            if team == 1:
                self.player1_minion_infomation.append(
                    {"id": number, "race": race, "x": x, "y": y})
            if team == 2:
                self.player2_minion_infomation.append(
                    {"id": number, "race": race, "x": x, "y": y})
            self.game.choose_deploy_minion(race, number, team, x, y)

    def send_minions(self):
        """
        クライアントにminionsの位置情報を送る。
        self.player1_minion_list,self.player2_minion_listのデータを送る。
        """
        minion_dict = {"player1": self.player1_minion_infomation,
                       "player2": self.player2_minion_infomation}
        return minion_dict

    def execute_game(self):
        """
        ゲームの実行
        """
        while(self.game.win == 0):
            self.log.append((self.time, self.game))
            self.time += 1
            self.game.time_evolve()
            if self.time > 1000:
                print("time is up")
                print(len(self.game.player1_alive_minions_list))
                print(len(self.game.player2_alive_minions_list))
                break

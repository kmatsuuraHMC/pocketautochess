from player import Player 
class GameObject(Player):
    """
    ゲームの状態空間（スナップショット）
    """

    def __init__(self):
        self.player1 = Player()
        self.player2 = Player()
        self.state = 0

    
    def setPlayerName(self, player_nunmber, player_name):
        if player_nunmber == 1:
            self.player1.name = player_name
        elif player_nunmber == 2:
            self.player2.name = player_name
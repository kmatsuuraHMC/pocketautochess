class Player:
    """
    プレイヤーについての情報
    Parameters
    ---------------
    name: str
    key: int
    """
    def __init__(self, name):
        self.name = name
        self.key = -1
        self.minion_list = []
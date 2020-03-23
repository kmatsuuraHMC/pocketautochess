class Minion:
    """
    ミニオンの持つべき挙動を決める親クラス

    Attributes
    --------------
    id,
    name:str,
    hp,
    atk,
    range,
    speed,
    state:str,
    positon:list
        numpy.arrayとかに変えたいあとで
    team:int,
    """
    #それぞれのミニオンに割り振れる一意なid
    id = 0
    default_hp = 100
    default_atk = 5
    default_range = 6
    default_speed = 20
    default_position = [0, 0]

    def __init__(self):
        self.id = id
        id += 1
        self.hp = default_hp 
        self.atk = default_atk
        self.range = default_range
        self.speed = default_speed
        self.name = ""
        self.positon = default_position
        self.team = 0

            

    


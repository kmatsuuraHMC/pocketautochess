
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
    default_hp = 100
    default_atk = 5
    default_range = 6
    default_speed = 20
    default_positionx = 0
    default_positiony = 0
    INF = 100000000

    def __init__(self, id):
        self.id = id
        self.hp = default_hp 
        self.atk = default_atk
        self.range = default_range
        self.speed = default_speed
        self.race = "minion"
        self.action = "move" #"attack" or "move" or "die"
        self.positonx = default_positionx
        self.positony = default_positiony
        self.team = 0
        self.target = None
        self.target_distance = INF

    def attack(self):
        """
        targetに向かって攻撃する。
        Attribute
        ----------------
        target: Minion
        """
        if self.range >= self.distance:
            self.target.hp -= self.atk

    def calc_distance(self, target = None):
        """
        targetとの距離を計算
        """
        target = self.target
        if target == None:
            return INF
        distance = ((self.positonx - target.positionx)**2 + (self.positony - target.positiony)**2)**0.5
        return distance


    def choose_target(self, minion_list):
        """
        targetを選択するメソッド
        Attribute
        ----------------
        minion_list: list<Minion>
        """
        target = self.target
        for minion in minion_list:
            distance = self.calc_distance(minion)
            if distance < self.target_distance:
                self.target_distance = distance
                self.target = target

    def move(self):
        """
        targetに向かって移動するメソッド
        """
        if self.target == None:
            return
        vectorx = self.target.positonx - self.positionx
        vectory = self.target.positony - self.positiony
        distance = self.calc_distance()
        self.positonx = self.speed * (vectorx / distance)
        self.positony = self.speed * (vectory / distance)

    def show_status(self):
        """
        デバック用、ステータスの確認
        """
        return "ID:{0}HP:{1}, ATK:{2}".format(self.id, self.hp, self.atk)

class Gagoiru(Minion):
    """
    Gagoiruクラス
    """
    default_hp = 3000
    default_atk = 20
    default_range = 6
    default_speed = 3
    default_positionx = 0
    default_positiony = 0
    INF = 100000000

    def __init__(self, id):
        super().__init__(id)
        self.race = "gagoiru"

class Gobrui(Minion):
    """
    Goburuiクラス
    """
    default_hp = 9000
    default_atk = 13
    default_range = 1.5
    default_speed = 10
    default_positionx = 0
    default_positiony = 0
    INF = 100000000

    def __init__(self, id):
        super().__init__(id)
        self.race = "goburui"

class Maruta(Minion):
    """
    Marutaクラス
    """
    default_hp = 1500
    default_atk = 10
    default_range = 3
    default_speed = 4
    default_positionx = 0
    default_positiony = 0
    INF = 100000000

    def __init__(self, id):
        super().__init__(id)
        self.race = "maruta"


    def attack(self):
        """
        範囲攻撃（未実装）
        """
        pass
        



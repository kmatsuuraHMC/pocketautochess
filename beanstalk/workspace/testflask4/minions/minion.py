
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
    INF = 100000000

    def __init__(self, id, hp, atk, range, speed):
        INF = 100000000
        self.id = id
        self.hp = hp
        self.atk = atk
        self.range = range
        self.speed = speed
        self.race = "minion"
        self.action = "move"  # "attack" or "move" or "die"
        self.positonx = 0
        self.positony = 0
        self.team = 0
        self.target = None
        self.target_distance = 100000000

    def attack(self):
        """
        targetに向かって攻撃する。
        Attribute
        ----------------
        target: Minion
        """
        if self.range >= self.distance:
            self.target.hp -= self.atk

    def calc_distance(self, target=None):
        """
        targetとの距離を計算
        """
        target = self.target
        if target == None:
            return INF
        distance = ((self.positonx - target.positionx)**2 +
                    (self.positony - target.positiony)**2)**0.5
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

    def __init__(self, id):
        super().__init__(id, hp=3000, atk=20, range=6, speed=3)
        self.race = "gagoiru"


class Gobrui(Minion):
    """
    Goburuiクラス
    """

    def __init__(self, id):
        super().__init__(id, hp=9000, atk=13, range=1.5, speed=10)
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
        super().__init__(id, hp=1500, atk=10, range=3, speed=4)
        self.race = "maruta"

    def attack(self):
        """
        範囲攻撃（未実装）
        """
        pass


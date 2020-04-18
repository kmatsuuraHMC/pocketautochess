
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
    position:list
        numpy.arrayとかに変えたいあとで
    team:int,
    """
    INF = 100000000

    def __init__(self, id, hp, atk, range, speed):
        self.id = id
        self.hp = hp
        self.atk = atk
        self.range = range
        self.speed = speed
        self.race = "minion"
        self.action = "stay"  # "attack" or "move" or "stay"
        self.state = "alive"  # "dead" or "alive" 今の所使わない、将来的に状態異常などが出てきたら使うかも
        self.positionx = 0.
        self.positiony = 0.
        self.team = 0
        self.target = None
        self.target_distance = Minion.INF  # distance between self and target

    def attack(self):
        """
        targetに向かって攻撃する。
        Attribute
        ----------------
        target: Minion
        """
        if self.range >= self.target_distance:
            self.target.hp -= self.atk
            if self.target.hp <= 0:
                self.target = None
                self.target_distance = Minion.INF

    def calc_distance(self, target):
        """
        targetとの距離を計算
        """
        if target is None:
            return Minion.INF
        vectorx = target.positionx - self.positionx
        vectory = target.positiony - self.positiony
        distance = (vectorx**2 + vectory**2)**0.5
        return distance

    def choose_target(self, minion_list):
        """
        targetを選択するメソッド、一番距離が近い相手をtargetにする。
        Attribute * distance
        ----------------
        minion_list: list<Minion>
        """
        self.target_distance = Minion.INF
        for minion in minion_list:
            distance = self.calc_distance(minion)
            if distance < self.target_distance:
                self.target_distance = distance
                self.target = minion

    def set_action(self):
        if self.target is None:
            self.action = "stay"
        elif self.target_distance <= self.range:
            self.action = "attack"
        else:
            self.action = "move"

    def move(self):
        """
        targetに向かって移動するメソッド
        """
        if self.target is None:
            return
        vectorx = self.target.positionx - self.positionx
        vectory = self.target.positiony - self.positiony
        distance = self.calc_distance(self.target)
        if distance <= self.range:
            return
        if (distance - self.range) < self.speed:
            move = (distance - self.range) * 0.5
        else:
            move = self.speed
        self.positionx += move * (vectorx / distance)
        self.positiony += move * (vectory / distance)

    def get_status(self):
        """
        デバック用、ステータスの確認
        """
        try:
            return {"team": self.team, "id": self.id, "hp": self.hp,
                    "atk": self.atk, "x": self.positionx, "y": self.positiony,
                    "target": (self.target.team, self.target.id, self.target.race), 
                    "target_distance": self.target_distance,
                    "target_position": (self.target.positionx, self.target.positiony),
                    "action": self.action}
        except:
            return {"team": self.team, "id": self.id, "hp": self.hp,
                    "atk": self.atk, "x": self.positionx, "y": self.positiony,
                    "target": self.target, 
                    "target_distance": self.target_distance,
                    "action": self.action}


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

    def __init__(self, id):
        super().__init__(id, hp=1500, atk=10, range=3, speed=4)
        self.race = "maruta"

    def attack(self):
        """
        範囲攻撃（未実装）
        """
        pass

#import re
import sys
input = sys.stdin.readline
#import heapq
#import bisect
#from collections import deque
#import math

class Unit:
    def __init__(self):
        self.name = None
        self.pos = None
        self.hp = None
        self.atk = None
        self.spd = None
        self.target = None
        return
    
    def print_status(self):
        print("Name:{}".format(self.name))
        print("Position:{}".format(self.pos))
        print("HP:{}".format(self.hp))
        print("ATK:{}".format(self.atk))
        print("SPEED:{}".format(self.spd))
        print("Target:{}".format(self.target))
        print("-"*50)
    
    
#import re
import sys
input = sys.stdin.readline
from unitclass import Unit
#import heapq
#import bisect
#from collections import deque
#import math

def main():
    readinput()

#read init data
def readinput():
    n1, n2 = map(int, input().split())
    team1 = [Unit()] * n1
    team2 = [Unit()] * n2
    print("="*50)
    print("teamA")
    print("="*50)
    for i in range(n1):
        team1[i].name = input().strip()
        team1[i].pos = list(map(int, input().split()))
        team1[i].hp = int(input())
        team1[i].atk = int(input())
        team1[i].spd = int(input())
        team1[i].print_status() 
    print("="*50)
    print("teamB")
    print("="*50)
    for i in range(n2):
        team2[i].name = input().strip()
        team2[i].pos = list(map(int, input().split()))
        team2[i].hp = int(input())
        team2[i].atk = int(input())
        team2[i].spd = int(input())
        team2[i].print_status()
    


    
if __name__ == '__main__':
    main()

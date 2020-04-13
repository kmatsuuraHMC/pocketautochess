from gamecontroller import GameObject, GameController
import json

def main():
    with open("sample_input.json") as f:
        data = json.load(f)
    
    gamecon = GameController()
    gamecon.player1.key = 5
    gamecon.player2.key = 6
    gamecon.deploy_minions(data)
    for minion in gamecon.game.player1_alive_minions_list:
        print("race:" + str(minion.race), minion.positionx, minion.positiony)

# run the app.
if __name__ == "__main__":
    main()
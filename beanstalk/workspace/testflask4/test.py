from gamecontroller import GameController
import json


def main():
    gamecon = GameController()
    gamecon.player1.key = 5
    gamecon.player2.key = 6
    for i in range(2):
        for j in range(3):
            filename = "sample_input/p{0}d{1}.json".format(i+1, j+1)
            with open(filename) as f:
                data = json.load(f)
            gamecon.deploy_minions(data)
    gamecon.execute_game()
    print("win:{}".format(gamecon.game.win))


# run the app.
if __name__ == "__main__":
    main()

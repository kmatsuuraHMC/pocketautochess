from flask import Flask, request, session, escape, jsonify
import json
from gamecontroller import GameController

# declare variable

# main body of the game
game = GameController()
# waiting user of the game
user_list = []
# generated_key_list
generated_key_list = []
generated_key_number = 0
# matchした人
match = -1
# matching中のペア
matching = []

# print a nice greeting.


def say_hello(username="World"):
    return '<p>Hello %s!</p>\n' % username


# some bits of text for the page.
header_text = '''
    <html>\n<head> <title>Pocket Auto Chess</title> </head>\n<body>'''
instructions = '''
    <p><em>Hint</em>: This is a RESTful web service!</p>\n'''
home_link = '<p><a href="/">Back</a></p>\n'
footer_text = '</body>\n</html>'

# EB looks for an 'application' callable by default.
application = Flask(__name__)


@application.route('/')
def index():
    if 'username' in session:
        return 'Logged in as %s' % escape(session['username'])
    return 'You are not logged in'


@application.route('/genekey', methods=["GET"])
def get_key():
    global generated_key_number
    key = generated_key_number
    generated_key_number += 1
    return str(key)


@application.route('/gamelobby', methods=["POST"])
def gamecontrol():
    """
    マッチングをして、ゲームを始めてしまう。あとでリファクタリングする。
    """
    global game
    data = request.json
    key = int(data['key'])

    # gameの初期化
    if key == -1:
        game.state = 0
        return"game.stateを{}に初期化しました。".format(game.state)

    # 排他処理
    if key not in [game.player1.key, game.player2.key] and game.state >= 2:
        return "Please wait for the current game ending"

    # 待ってる人がいない。
    if game.state == 0:
        game.player1.key = key
        game.state = 1
        return "You are player1. Waiting for an opponent."

    # すでに待ってる人がいる
    if game.state == 1:
        if key == game.player1.key:
            return "Waiting for an opponent."
        game.player2.key = key
        game.state = 2
        return "You are player2. Game start"

    # ゲームスタート
    if game.state == 2:
        if key == game.player1.key:
            game.state = 3
            return "You are player1. Game start"
        elif key == game.player2.key:
            return "Waiting for the opponent"

    # 1回目のコマを配置するフェイズ
    if game.state == 3:
        if key == game.player1.key:
            if data["deploy"]["deployPhase"] != "1":
                return "deployPhaseが1でありません。"
            if game.player1_deploy == 1:
                return "waiting for opponent"
            # 1回目のコマの配置
            game.deploy_minions(data)
            if game.player2_deploy == 1:
                game.state = 4
            elif game.player2_deploy == 0:
                game.player1_deploy = 1
            return "player1's minions are set successfully!"

        if key == game.player2.key:
            if data["deploy"]["deployPhase"] != "1":
                return "deployPhaseが1でありません。"
            if game.player2_deploy == 1:
                return "waiting for opponet"
            # 1回目のコマの配置
            game.deploy_minions(data)
            if game.player1_deploy == 1:
                game.state = 4
            elif game.player1_deploy == 0:
                game.player2_deploy = 1
            return "player2's minions are set successfully!"

    # 1回目のコマの配置を相手に送信
    if game.state == 4:
        if key == game.player1.key:
            if game.player1_deploy == 2:
                return "waiting for opponent"
            # 1回目のコマのデータを送信
            to_client = game.send_minions()
            if game.player2_deploy == 2:
                game.state = 5
            elif game.player2_deploy == 1:
                game.player1_deploy = 2
            return json.dumps(to_client)

        if key == game.player2.key:
            if game.player2_deploy == 2:
                return "waiting for opponent"
            # 1回目のコマのデータを送信
            to_client = game.send_minions()
            if game.player1_deploy == 2:
                game.state = 5
            elif game.player1_deploy == 1:
                game.player2_deploy = 2
            return json.dumps(to_client)

    # 2回目のコマを配置するフェイズ
    if game.state == 5:
        if key == game.player1.key:
            if data["deploy"]["deployPhase"] != "2":
                return "deployPhaseが2でありません。"
            if game.player1_deploy == 3:
                return "waiting for opponent"
            # 2回目のコマの配置
            game.deploy_minions(data)
            if game.player2_deploy == 3:
                game.state = 6
            elif game.player2_deploy == 2:
                game.player1_deploy = 3
            return "player1's minions are set successfully!"

        if key == game.player2.key:
            if data["deploy"]["deployPhase"] != "2":
                return "deployPhaseが2でありません。"
            if game.player2_deploy == 3:
                return "waiting for opponet"
            # 2回目のコマの配置
            game.deploy_minions(data)
            if game.player1_deploy == 3:
                game.state = 6
            elif game.player1_deploy == 2:
                game.player2_deploy = 3
            return "player2's minions are set successfully!"

    # 2回目のコマの配置を相手に送信
    if game.state == 6:
        if key == game.player1.key:
            if game.player1_deploy == 4:
                return "waiting for opponent"
            # 2回目のコマのデータを送信
            to_client = game.send_minions()
            if game.player2_deploy == 4:
                game.state = 7
            elif game.player2_deploy == 3:
                game.player1_deploy = 4
            return jsonify(to_client)

        if key == game.player2.key:
            if game.player2_deploy == 4:
                return "waiting for opponet"
            # 2回目のコマのデータを送信
            to_client = game.send_minions()
            if game.player1_deploy == 4:
                game.state = 7
            elif game.player1_deploy == 3:
                game.player2_deploy = 4
            return jsonify(to_client)

    # 3回目のコマを配置するフェイズ
    if game.state == 7:
        if key == game.player1.key:
            if data["deploy"]["deployPhase"] != "3":
                return "deployPhaseが3でありません。"
            if game.player1_deploy == 5:
                return "waiting for opponent"
            # 3回目のコマの配置
            game.deploy_minions(data)
            if game.player2_deploy == 5:
                game.state = 8
            elif game.player2_deploy == 4:
                game.player1_deploy = 5
            return "player1's minions are set successfully!"

        if key == game.player2.key:
            if data["deploy"]["deployPhase"] != "3":
                return "deployPhaseが3でありません。"
            if game.player2_deploy == 5:
                return "waiting for opponet"
            # 3回目のコマの配置
            game.deploy_minions(data)
            if game.player1_deploy == 5:
                game.state = 8
            elif game.player1_deploy == 4:
                game.player2_deploy = 5
            return "player2's minions are set successfully!"

    # 3回目のコマの配置を相手に送信
    if game.state == 8:
        if key == game.player1.key:
            if data["deploy"]["deployPhase"] != "3":
                return "deployPhaseが3でありません。"
            if game.player1_deploy == 6:
                return "waiting for opponent"
            # 3回目のコマのデータを送信
            to_client = game.send_minions()
            if game.player2_deploy == 6:
                game.state = 9
            elif game.player2_deploy == 5:
                game.player1_deploy = 6
            return jsonify(to_client)

        if key == game.player2.key:
            if data["deploy"]["deployPhase"] != "3":
                return "deployPhaseが3でありません。"
            if game.player2_deploy == 6:
                return "waiting for an opponent"
            # 3回目のコマのデータを送信
            to_client = game.send_minions()
            if game.player1_deploy == 6:
                game.state = 9
            elif game.player1_deploy == 5:
                game.player2_deploy = 6
            return jsonify(to_client)

    # ゲームの実行
    if game.state == 9:
        # gameの実行(終了したら勝手にgame.state = 10にする)
        game.execute_game()
        game.state = 10
        return

    # ログを流す
    if game.state == 10:
        # お互いがログを受け取る
        game.state = 0


# set the secret key.  keep this really secret:
application.secret_key = 'A0Zr98j/3yX R~XHH!jmN]LWX/,?RT'


# run the app.
if __name__ == "__main__":
    # Setting debug to True enables debug output. This line should be
    # removed before deploying a production app.
    application.debug = False
    application.run()

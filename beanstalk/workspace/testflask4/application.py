from flask import Flask, request, redirect, url_for, session, escape, jsonify

from gamecontroller import GameObject, GameController

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
def say_hello(username = "World"):
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
    
    #gameの初期化
    if key == -1:
        game.state = 0
        return"game.stateを{}に初期化しました。".format(game.state)
    #待ってる人がいない。
    if game.state == 0:
        game.player1.key = key
        game.state = 1
        return "You are player1. Waiting for an opponent."
    

    #すでに待ってる人がいる
    if game.state == 1:
        if key == game.player1.key:
            return "Waiting for an opponent."
        game.player2.key = key
        game.state = 2
        return "You are player2. Game start"
    
    #ゲームスタート
    if game.state == 2:
        if key not in [game.player1.key, game.player2.key]:
            return "Please wait for the current game ending"
        elif key == game.player1.key:
            game.state = 3
            return "You are player1. Game start"
        elif key == game.player2.key:
            return "Waiting for the opponent"

    #1回目のコマを配置するフェイズ
    if game.state == 3:
        p1set = 0
        p2set = 0
        if key == game.player1.key:
            if p1set == 1:
                return "waiting for opponent"
            ##1回目のコマの配置
            if p2set == 1:
                game.state = 4
            elif p2set == 0:
                p1set = 1
            return

        if key == game.player2.key:
            if p2set == 1:
                return "waiting for opponet"
            ##1回目のコマの配置
            if p1set == 1:
                game.state = 4
            elif p1set == 0:
                p2set = 1
            return

    #1回目のコマの配置を相手に送信
    if game.state == 4:
        p1send = 0
        p2send = 0
        if key == game.player1.key:
            if p1send == 1:
                return "waiting for opponent"
            ##1回目のコマのデータを送信
            if p2send == 1:
                game.state = 5
            elif p2send == 0:
                p1send = 1
            return

        if key == game.player2.key:
            if p2send == 1:
                return "waiting for opponet"
            ##1回目のコマのデータを送信
            if p1send == 1:
                game.state = 5
            elif p1send == 0:
                p2send = 1
            return

    #2回目のコマを配置するフェイズ
    if game.state == 5:
        p1set = 0
        p2set = 0
        if key == game.player1.key:
            if p1set == 1:
                return "waiting for opponent"
            ##2回目のコマの配置
            if p2set == 1:
                game.state = 6
            elif p2set == 0:
                p1set = 1
            return

        if key == game.player2.key:
            if p2set == 1:
                return "waiting for opponet"
            ##2回目のコマの配置
            if p1set == 1:
                game.state = 6
            elif p1set == 0:
                p2set = 1
            return

    #2回目のコマの配置を相手に送信
    if game.state == 6:
        p1send = 0
        p2send = 0
        if key == game.player1.key:
            if p1send == 1:
                return "waiting for opponent"
            ##2回目のコマのデータを送信
            if p2send == 1:
                game.state = 7
            elif p2send == 0:
                p1send = 1
            return

        if key == game.player2.key:
            if p2send == 1:
                return "waiting for opponet"
            ##2回目のコマのデータを送信
            if p1send == 1:
                game.state = 7
            elif p1send == 0:
                p2send = 1
            return

    #3回目のコマの配置を相手に送信
    if game.state == 7:
        p1send = 0
        p2send = 0
        if key == game.player1.key:
            if p1send == 1:
                return "waiting for opponent"
            ##3回目のコマのデータを送信
            if p2send == 1:
                game.state = 8
            elif p2send == 0:
                p1send = 1
            return

        if key == game.player2.key:
            if p2send == 1:
                return "waiting for an opponent"
            ##3回目のコマのデータを送信
            if p1send == 1:
                game.state = 8
            elif p1send == 0:
                p2send = 1
            return
    
    #ゲームの実行
    if game.state == 8:
        ## gameの実行(終了したら勝手にgame.state = 9になる)
        return

    #ログを流す
    if game.state == 9:
        ## お互いがログを受け取る
        game.state = 0

@application.route("/posttest", methods=["GET", "POST"])
def odd_even():
    if request.method == "GET":
        return """
        下に整数を入力してください。奇数か偶数か判定します
        <form action="/" method="POST">
        <input name="num"></input>
        </form>"""
    else:
        return """
        {}は{}です！
        <form action="/posttest" method="POST">
        <input name="num"></input>
        </form>""".format(str(request.form["num"]), ["偶数", "奇数"][int(request.form["num"]) % 2])


# set the secret key.  keep this really secret:
application.secret_key = 'A0Zr98j/3yX R~XHH!jmN]LWX/,?RT'



# run the app.
if __name__ == "__main__":
    # Setting debug to True enables debug output. This line should be
    # removed before deploying a production app.
    application.debug = False
    application.run()

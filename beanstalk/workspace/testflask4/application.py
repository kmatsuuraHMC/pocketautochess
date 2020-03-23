from flask import Flask, request, redirect, url_for, session, escape, jsonify

from gamecontroller import GameObject

# declare variable
# main body of the game
game = GameObject()
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
    key = int(request.form['key'])
    
    if game.state == 0:
        game.player1.key = key
        game.state = 1
        return "You are player1. Waiting for an opponent."

    if game.state == 1:
        if key == game.player1.key:
            return "Waiting for an opponent."
        game.player2.key = key
        game.state = 2
        return "You are player2. Game start"
    
    if game.state == 2:
        if key not in [game.player1.key, game.player2.key]:
            return "Please wait for the current game ending"
        elif key == game.player1.key:
            game.state = 3
            return "You are player1. Game start"
        elif key == game.player2.key:
            return "Waiting for the opponent"

    
    if game.state == 3:
        if key == -1:
            game.state = 0
            return"game.stateを初期化しました。"
        return "コマを配置するフェイズ(未実装)。game.stateを初期化するためにはkey=-1でPOSTしてください。"



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

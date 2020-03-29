# pocketautochess

## 用語
ミニオン//プレイヤーが配置する駒（キャラ）のこと

## ゲームの流れ
- まずミニオンを並べる。
- フレームごとに確率でどれかのミニオンが攻撃。  
- フレームが終わったらhp == 0のミニオンを消す。  
- 生きてるミニオンが移動。  
- ミニオンがなくなった方の負け。  

\\まだ書いてる途中
## 実装(`/beanstalk/workspace/testflask4/`)
# class GameController
## field

### class GameObject //ゲームの状態空間(snapshot)
#### field
- teammate //キャラの配列
#### method
- add //ミニオンの追加
- delete //ミニオンの削除

### class Minion //キャラ
#### field
- hp
- positionx //x座標
- positiony //y座標
- atk
- range //攻撃範囲
- speed //移動速度
- id //個体番号
- race //ミニオンの種類(ex. maruta)
- target //攻撃対象<Minion>
- target_distance //targetとの距離
- team //所属しているチーム
#### method
- choose_traget(minion_list) //minion_listの中からtargetを選択
- attack //攻撃する
- move //移動する

# サーバーとクライアントの通信について
## 全体的な流れ
クライアントが\gamelobbyにPOSTし続ける。  
key(user idのようなもの)を入手→keyを使ってマッチング→ミニオンの配置→サーバーから対戦相手のミニオンの配置の情報を受け取る。→サーバーの中で戦闘の計算→サーバーからログを受け取る。

## マッチングまで
クライアント: POST keyをし続けたらマッチングしてくれる。マッチングするまでは"マッチング中です"マッチングしたら、"ゲームスタート"と返ってくる。

## ミニオンを配置するフェイズ
#### 1回目の配置
POST (key, minionの配置情報) //クライアント：10体分の情報を送る。(1~10体目)  
その後サーバーから相手のミニオンの情報が帰ってくるまでkeyをPOSTし続ける。  

#### 2回目の配置
POST (key, minionの配置情報) //クライアント：10体分の情報を送る。(11~20体目)  
その後サーバーから相手のミニオンの情報が帰ってくるまでkeyをPOSTし続ける。  

#### 3回目の配置
POST (key, minionの配置情報) //クライアント：10体分の情報を送る。(21~30体目)  
その後サーバーから相手のミニオンの情報が帰ってくるまでkeyをPOSTし続ける。  

## 戦闘フェイズ
サーバにて計算中。クライアントはPOST key をひたすらする。（もし計算が終わってたらログが帰ってくる。終わってなかったら、ちょっと待ってねと帰ってくる。）

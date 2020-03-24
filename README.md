# pocketautochess

## ルール

### class team //キャラの集合
#### field
- teammate //キャラの配列
#### method
- add //キャラの追加
- delete //キャラの削除

### class character
#### field
- hp
- position //座標
//あとで松崎くんが編集する
- atk
- id //個体番号
#### method
- setAttackTarget //攻撃対象を選ぶ
- attack //攻撃する
- moveTo //移動する

## ゲームの流れ
- まずユニットを並べる。
- フレームごとに確率でどれかのユニットが攻撃。  
- フレームが終わったらhp == 0のユニットを消す。  
- 生きてるユニットが移動。  
- ユニットがなくなった方の負け。  



びるどほうほう
nodeServerをビルドしてください。nodeServerにビルド方法が書いてあります（まる）
gobrui1をビルドしてください。gobrui1のFSharpをビルドする必要が有ります。
cd ./gobrui1/FSharp
ここのreadmeを参照して下しい

# サーバーとクライアントの通信について
クライアントが\gamelobbyにPOSTし続ける。
## マッチングまで
クライアント: POST

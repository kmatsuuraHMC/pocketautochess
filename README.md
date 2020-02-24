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
- フレームごとに確率でどれかのユニットが攻撃
‐ フレームが終わったらhp == 0のユニットをけす。
- ユニットがなくなった方の負け。

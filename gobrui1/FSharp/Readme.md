git@github.com:fsprojects/FSharpx.Extras.git

ionide推奨

GameObjectにくっつけたい -> CSharpで同一名のクラスを作成し継承（空で問題ない）それをオブジェクトに当ててください
初期条件でコルーチンが使いたい(初期条件から非同期で処理してほしい) -> StartFuncという名前でコルーチンを返すメンバーを作り、CSharp側でつける(俺の技術力の限界でC#側から設定せざるを得なかった)
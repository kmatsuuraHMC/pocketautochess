git@github.com:fsprojects/FSharpx.Extras.git

ionide推奨

ビルド↓
cp -r $HOME/Unity/Hub/Editor/2019.3.2f1/Editor/Data ./
dotnet build

他、packageのところにMiniJsonの奴入ってるのでビルドしてください。
cd package
cd MiniJson
dotnet build
これでMiniJsonができます。なんというか、我の怠慢でgobrui1のAssetsの中にプラグイン入れ忘れてたので、MiniJson書き換えたければMiniJson.dllをassetsんいコピペ。

★ビルドできない時
Mono-completeを必死に入れる。
https://www.mono-project.com/download/stable/#download-lin
dotnet cliを必死に入れる。
https://docs.microsoft.com/ja-jp/dotnet/core/install/linux-package-manager-ubuntu-1804

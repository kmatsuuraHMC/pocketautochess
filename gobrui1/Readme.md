
``` shellscript
https://github.com/Cysharp/UniTask/releases これのv1.2.0をAssetsでclone -> Unityを開くことでコンパイルされる

cp -r ~/Unity/Hub/Editor/2019.3.2f1/Editor/Data FSharp/ ./
FSharpで`dotnet build` →失敗するなら、mono-developのバージョンを確認する(Mono JIT compiler version 6.8.0.105 )
```
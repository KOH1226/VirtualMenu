# VirtualMenu

## 前書き

本アプリケーションはHMD(Oculus CV1, Oculus Quest)および  
Leapmotionを利用する前提で開発したものであり、  
その他の環境での動作確認は行っておりません。  
開発環境としてUnity2019.2.13f1およびUnity2019.1.5f1を用いており、  
LeapmotionはLeap_Motion_Developer_Kit_4.0.0+52173のSDKを利用しています。  
これらのデバイスや環境のバージョンが異なった場合は動作しない可能性があります。  
また、このREADMEはUnityでのVR開発経験があることを前提として書いている部分があるため、  
一度はVR開発やUnityでの開発に触れておくことを推奨します。  
Leapmotionに関しては開発者向けの公式HP(https://developer.leapmotion.com/)に  
SDKやリファレンスがあるため、それらを参考にして頂ければと思います。  

以上のことに留意して、本アプリケーションを利用してください。


## 概要

本研究のアプリケーションに関するフォルダは  
OneHand4menuには提案手法を用いた片手操作用仮想メニューのUnityプロジェクトが存在します。  
ComparisonMenuには評価実験用に作成した比較用既存仮想メニューのUnityプロジェクトが存在します。  

Unityプロジェクトに関してはUnityを用いてプロジェクトを開いて頂ければ利用できます。  
利用する際はUnityの設定でVRおよびLeapmotionでの開発が可能な状態にしてください。  

## ソースコードについて

ソースコードは各UnityプロジェクトのScriptフォルダに格納されています。  
変数や関数には記述されているコメントを参考にしてソースコード内での役割を把握して頂けたらと思います。  
なお、Debug.Logなどで記述されているものに関しては開発時の動作確認用のものであるため、  
アプリケーション内での役割は基本的にありません。不要であれば削除してください。  
Unityプロジェクト内で利用していないソースコードについても、開発中に試験的に記述したものであるため、  
不要であれば削除してください。  

なお、プロジェクトは実験用に修正されており、特定のフォルダへ実験結果を書き込む仕様になっています。  
操作時のログを別ファイルで保存したい場合、該当コードのフォルダ名を任意のフォルダ名に変更してください。  


## 操作方法

### OneHand4menu

本プロジェクトは、提案手法を用いた片手操作用仮想メニューです。  
操作には左手のみを利用し、メニューは人差し指と親指を開くことによって表示されます。  
項目の選択時には、人差し指の方向を変えることで項目を変更し、  
親指を一度だけ閉じるジェスチャ操作で項目を決定することができます。  
親指を閉じ続けるとその項目を連打（選択し続ける）判定となるため、選択する際には素早く開閉してください。  
メニューは人差し指と親指を閉じることによって非表示にすることが可能です。  

### GestureMenu

本プロジェクトは、評価実験用に作成したジェスチャ操作用仮想メニューです。  
正面に大きく表示されたメニューに対して、右手の指の開いている指に対応した項目を選択することができます。  
親指のみ開いている場合はA、親指・人差し指が開いている場合はB、親指・人差し指・中指を開いている場合はC、  
全ての指を開いている場合はDを選択できる状態になります。  
項目決定についてはOneHand4menuと同じく、親指を閉じるジェスチャ操作で行えます。  

### LaserMenu

本プロジェクトは評価実験用に作成したレーザーポインタ操作用仮想メニューです。  
正面に大きく表示されたメニューに対して、  
右手の人差し指から描写されるレーザーポインタを用いて項目を選択することができます。  
レーザポインタを項目に向け、OneHand4menuと同じく、  
親指を閉じるジェスチャ操作を行うことで項目を決定することが可能です。  
左のパネルには、一つ前に選択した項目が表示されます。  

実験用であるため、右に表示されたパネルで項目を指定されていますが、無視して頂いて構いません。  

## 緊急時の復旧方法

前提条件として、Unityエディタが停止するなどの不具合が生じた場合、その後絶対にプロジェクトをそのまま再度開かないでください。  
一度でも立ち上げた場合、この方法で復旧することは不可能です。  
復旧後は、最後に保存した状態もしくは最後にUnityプロジェクトを再生した状態に復旧できます。  

Unityエディタ内でクラッシュや無限ループが起こった場合、以下の通りに復旧してください。多分復旧できます。  

1．Unityプロジェクトのフォルダへ移動  
2．Temp/__Backupscenesフォルダを検索し、該当フォルダをコピー   
3．該当フォルダをTempフォルダ以外へ退避（ペースト）  
4．ここでUnityエディタを起動し、最後に開いていたシーンを同じように開く  
5．ゲームを再生  
6．再生中に__Backupscenesフォルダを元の場所に上書きする  
7．再生を停止  

以上の手順で復旧が可能ですが、バージョンによって復旧できない可能性があるため、こまめに保存することを心掛けてください。  

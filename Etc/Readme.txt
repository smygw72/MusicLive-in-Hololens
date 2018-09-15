【使い方】
UI->Button->UI Button Listより
・音源の名前の入力
・Buttonオブジェクトの指定
を行う．
※ ただし，音源の名前とAudio Clipの名前と一致する必要がある．

【内部処理】
1. シーン読み込み
2. Public/Private SpeakerRegister.csでAudioSourceを登録
3. UI Public/Private ChannelButtonList.csでボタンのイベント登録

【注意点】
Findで呼び出されるゲームオブジェクト
・PrivateSpeakers
・PublicSpeakers
・PrivateChannelManager
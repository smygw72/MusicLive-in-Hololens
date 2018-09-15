using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SingletonSceneManager : SingletonMonoBehaviour<SingletonSceneManager>
{
    PublicSpeakerRegister publicSpeakerRegister;
    PrivateSpeakerRegister privateSpeakerRegister;

    UIPublicChannelButtonList uiPublicChannelButtonList;
    UIPrivateChannelButtonList uiPrivateChannelButtonList;
    UIMusicButtonList uiMusicButtonList;

    SingletonPositionManager singletonPositionManager;

    // 基底クラスのAwakeを呼び出せるようにするためnewをつける
    new private void Awake()
    {
        base.Awake();
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 曲の入れ替え
        publicSpeakerRegister = GameObject.Find("PublicSpeaker").GetComponent<PublicSpeakerRegister>();
        privateSpeakerRegister = GameObject.Find("PrivateSpeakers").GetComponent<PrivateSpeakerRegister>();
        publicSpeakerRegister.OnSceneLoaded_SwitchAudioClip(scene, LoadSceneMode.Single);
        privateSpeakerRegister.OnSceneLoaded_AddAudioSource(scene, LoadSceneMode.Single);

        // ボタンのイベント登録
        uiPublicChannelButtonList = GameObject.Find("PublicChannelManager").GetComponent<UIPublicChannelButtonList>();
        uiPublicChannelButtonList.OnSceneLoaded_RegisterButtonEvent();

        uiPrivateChannelButtonList = GameObject.Find("PrivateChannelManager").GetComponent<UIPrivateChannelButtonList>();        
        uiPrivateChannelButtonList.OnSceneLoaded_RegisterButtonEvent();

        uiMusicButtonList = GameObject.Find("MusicManager").GetComponent<UIMusicButtonList>();
        uiMusicButtonList.OnSceneLoaded_RegisterButtonEvent(scene, LoadSceneMode.Single);

        // PublicPositionをロードする(引き継ぐ)
        singletonPositionManager = GameObject.Find("PositionManager").GetComponent<SingletonPositionManager>();
        singletonPositionManager.OnSceneLoaded_LoadPublicSpeakerPosition();
    }

    private void OnSceneUnloaded(Scene scene)
    {
        // PublicPositionをセーブする
        //singletonPositionManager.OnSceneUnloaded_SavePublicSpeakerPosition();
    }
}

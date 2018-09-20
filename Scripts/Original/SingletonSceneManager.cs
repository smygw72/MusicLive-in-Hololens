using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SingletonSceneManager : SingletonMonoBehaviour<SingletonSceneManager>
{
    PublicSpeakerController publicSpeakerRegister;
    PrivateSpeakerController privateSpeakerRegister;

    UIPublicChannelButtonList uiPublicChannelButtonList;
    UIPrivateChannelButtonList uiPrivateChannelButtonList;
    UIMusicButtonList uiMusicButtonList;

    SingletonPositionManager singletonPositionManager;

    // 基底クラスのAwakeを呼び出せるようにするためnewをつける
    private void Awake()
    {
        Debug.Log("ScneManager Awake!");
        if (this != Instance)
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // 依存関係があるので順番大事！！！！
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 曲の入れ替え
        publicSpeakerRegister = GameObject.Find("PublicSpeaker").GetComponent<PublicSpeakerController>();
        privateSpeakerRegister = GameObject.Find("PrivateSpeakers").GetComponent<PrivateSpeakerController>();
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
}

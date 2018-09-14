using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PublicSpeakerRegister : MonoBehaviour {

    private string musicName, publicName;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded_SwitchAudioClip;
    }

    // シーンを読み込むたびにAudioClipを入れ替える
    private void OnSceneLoaded_SwitchAudioClip(Scene scene, LoadSceneMode mode)
    {
        musicName = scene.name;
        Debug.Log(musicName + "シーンでpublicSpeakerの登録を行います");

        UIPublicChannelButtonList uiPublicChannelButtonList = GameObject.Find("PublicChannelManager").GetComponent<UIPublicChannelButtonList>();

        publicName = uiPublicChannelButtonList.publicName;

        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.clip = Resources.Load(musicName +"/" + publicName) as AudioClip;
        if (audioSource.clip.name == publicName) Debug.Log(publicName + "が登録されました");

        // AudioSourceの初期設定
        audioSource.spatialBlend = 1f;
        audioSource.Play();

        // ボタンのイベント登録
        uiPublicChannelButtonList.OnSpeakerRegistered();
    }
}

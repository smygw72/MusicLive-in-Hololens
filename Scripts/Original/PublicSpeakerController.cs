using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PublicSpeakerController : MonoBehaviour {

    private string musicName, publicName;
    AudioSource audioSource;

    // シーンを読み込むたびにAudioClipを入れ替える
    public void OnSceneLoaded_SwitchAudioClip(Scene scene, LoadSceneMode mode)
    {
        musicName = scene.name;
        Debug.Log(musicName + "シーンでpublicSpeakerの登録を行います");

        UIPublicChannelButtonList uiPublicChannelButtonList = GameObject.Find("PublicChannelManager").GetComponent<UIPublicChannelButtonList>();

        publicName = uiPublicChannelButtonList.publicName;

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = Resources.Load(musicName +"/" + publicName) as AudioClip;
        if (audioSource.clip.name == publicName) Debug.Log(publicName + "が登録されました");

        // AudioSourceの初期設定
        audioSource.spatialBlend = 1f;
    }

    public void PlayOrPause(Text text)
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
            text.text = "Play!!";
        }
        else
        {
            audioSource.Play();
            text.text = "Pause";
        }
    }
}

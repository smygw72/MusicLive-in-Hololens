using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrivateSpeakerRegister : MonoBehaviour
{
    private string musicName, privateName;
    UIPrivateChannelButtonList uiPrivateChannelButtonList;
    List<UIPrivateChannelButtonList.PrivateChannel> privateChannelList;


    // シーンを読み込むたびにAudioClipを入れ替える
    public void OnSceneLoaded_AddAudioSource(Scene scene, LoadSceneMode mode)
    {
        musicName = scene.name;
        Debug.Log(musicName + "シーンでPrivateSpeakerの登録を行います");

        uiPrivateChannelButtonList = GameObject.Find("PrivateChannelManager").GetComponent<UIPrivateChannelButtonList>();

        privateChannelList = uiPrivateChannelButtonList.PrivateChannelList;

        foreach(UIPrivateChannelButtonList.PrivateChannel privateChannel in privateChannelList)
        {
            privateName = privateChannel.name;

            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = Resources.Load(musicName + "/" + privateName) as AudioClip;
            if (audioSource.clip.name == privateName) Debug.Log(privateName + "が登録されました");

            // AudioSourceの初期設定
            audioSource.spatialBlend = 1f;
            audioSource.Play();
        }
    }
}

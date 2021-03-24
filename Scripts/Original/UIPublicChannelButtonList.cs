using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class UIPublicChannelButtonList : MonoBehaviour
{
    public string publicName;
    public Button publicBtn;
    private AudioSource publicSource;

    public void OnSceneLoaded_RegisterButtonEvent()
    {
        publicSource = GameObject.Find("PublicSpeaker").GetComponent<AudioSource>();

        // Error処理
        if (publicSource == null) Debug.LogError(publicName + "にAudioSourceが割り当てられませんでした");

        // イベントを登録
        if (publicBtn != null)
        {
            //publicBtn.onClick.AddListener(() => OnClick_Log());
            publicBtn.onClick.AddListener(() => OnClick_TurnCond());

            // ボタンの名前をnameにする
            Text text;
            text = publicBtn.transform.Find("Text").GetComponent<Text>();
            text.text = publicName + "(Disable?)";
        }
    }

    #region "CallBack Function"
    private void OnClick_Log()
    {
        Debug.Log(publicName);
    }
    private void OnClick_TurnCond()
    {
        Text text;
        text = publicBtn.transform.Find("Text").GetComponent<Text>();

        if (publicSource.mute)
        {
            //Debug.Log("OFF");
            publicSource.mute = false;
            text.text = publicName + "(Disable?)";
        }
        else
        {
            //Debug.Log("ON");
            publicSource.mute = true;
            text.text = publicName + "(Enable?)";
        }
    }
    #endregion
}
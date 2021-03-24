using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerButton : MonoBehaviour {

    private PublicSpeakerController publicSR;
    private PrivateSpeakerController privateSR;
    private Text text;

    private void Awake()
    {
        // イベントを登録
        Button btn = GetComponent<Button>();
        if (btn == null)
        {
            Debug.LogError("PlayerButtonがnullです");
        }
        //btn.onClick.AddListener(() => OnClick_Log());
        btn.onClick.AddListener(() => OnClick_PlayOrStop());

        // ボタンの名前を変更
        text = btn.transform.Find("Text").GetComponent<Text>();
        text.text = "Play!!";

        publicSR = GameObject.Find("PublicSpeaker").GetComponent<PublicSpeakerController>();
        privateSR = GameObject.Find("PrivateSpeakers").GetComponent<PrivateSpeakerController>();

    }

    #region "CallBack"
    private void OnClick_Log()
    {
        Debug.Log("Playerが押されました");
    }
    private void OnClick_PlayOrStop()
    {
        publicSR.PlayOrPause(text);
        privateSR.PlayOrPause(text);
    }
    #endregion
}

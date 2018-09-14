using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class UIPrivateChannelButtonList : MonoBehaviour
{
    [System.Serializable]
    public class PrivateChannel
    {
        public string name;
        public Button btn;
        [System.NonSerialized] public AudioSource audioSource;

        public PrivateChannel(Button btn)
        {
            name = "";
            this.btn = btn;
            audioSource = null;
        }
    }

    [SerializeField]
    private List<PrivateChannel> privateChannelList;
    public List<PrivateChannel> PrivateChannelList
    {
        get { return privateChannelList; }
        private set { privateChannelList = value; }
    }

    // PrivateSpeakerRegisterで呼ばれる
    public void OnSpeakerRegistered()
    {
        AudioSource[] audioSourceArr = GameObject.Find("PrivateSpeakers").GetComponents<AudioSource>();

        foreach (PrivateChannel pc in privateChannelList)
        {
            // AudioSourceの割り当て
            foreach(AudioSource audioSorce in audioSourceArr)
            {
                if (audioSorce.clip.name == pc.name)
                {
                    pc.audioSource = audioSorce;
                }
            }
            if (pc.audioSource == null) Debug.Log(name + "にAudioSourceが割り当てられませんでした");

            // コールバック関数を登録
            Button btn = pc.btn;
            if (btn == null) { continue; }
            //btn.onClick.AddListener(() => OnClick_Log(pc.name));
            btn.onClick.AddListener(() => OnClick_SwitchMute(pc.audioSource, pc.btn, pc.name));

            // ボタンの名前を変更
            Text text;
            text = btn.transform.Find("Text").GetComponent<Text>();
            text.text = pc.name + "(Disable?)";
        }
    }

    private void Setup()
    {
        // 子供にいるボタンを列挙
        Button[] btnList = transform.GetComponentsInChildren<Button>();

        if (privateChannelList == null) {
            privateChannelList = new List<PrivateChannel>();
        }
        foreach (Button btn in btnList)
        {
            if (privateChannelList.FindIndex(x => x.btn == btn) < 0)
            {
                privateChannelList.Add(new PrivateChannel(btn));
            }
        }

        // ボタンが無くなっていたらリストから削除
        for (int i = privateChannelList.Count - 1; i >= 0; i--)
        {
            if (privateChannelList[i].btn == null)
            {
                privateChannelList.RemoveAt(i);
            }
        }
    }

    //public Button Get(string key)
    //{
    //    // キーで取得.
    //    PrivateSound ps = privateSoundList.Find(x => x.name == key);
    //    if (ps == null) { return null; }
    //    return ps.btn;
    //}

    #region CallBack Function
    private void OnClick_Log(string key)
    {
        Debug.Log(key);
    }
    private void OnClick_SwitchMute(AudioSource @as, Button btn, string name)
    {
        Text text;
        text = btn.transform.Find("Text").GetComponent<Text>();

        if (@as.mute)
        {
            @as.mute = false;
            text.text = name + "(Disable?)";
        }
        else
        {
            @as.mute = true;
            text.text = name + "(Enable?)";
        }
    }
    #endregion

#if UNITY_EDITOR
    [CustomEditor(typeof(UIPrivateChannelButtonList))]
    public class UIPrivateChannelButtonListEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            UIPrivateChannelButtonList btnList = target as UIPrivateChannelButtonList;
            if (GUILayout.Button("Setup"))
            {
                btnList.Setup();
            }

            base.OnInspectorGUI();
        }
    }
#endif
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

// test

public class UIButtonList : MonoBehaviour
{
    [System.Serializable]
    private class Sound
    {
        public string name;
        public Button btn;
        [System.NonSerialized] public AudioSource audioSource;

        public Sound(Button btn)
        {
            name = "";
            this.btn = btn;
            audioSource = null;
        }
    }

    [SerializeField]
    private List<Sound> SoundList;

    void Awake()
    {
        AudioSource[] privateSourceArr = GameObject.Find("PrivateSounds").GetComponents<AudioSource>();
        AudioSource publicSource = GameObject.Find("PublicSound").GetComponent<AudioSource>();

        foreach (Sound sd in SoundList)
        {
            // AudioSourceの割り当て
            foreach(AudioSource privateSorce in privateSourceArr)
            {
                if (privateSorce.clip.name == sd.name)
                {
                    sd.audioSource = privateSorce;
                }
            }
            if (publicSource.clip.name == sd.name) sd.audioSource = publicSource;

            // Error処理
            if (sd.audioSource == null) Debug.Log(name + "にAudioSourceが割り当てられませんでした");

            // コールバック関数を登録
            Button btn = sd.btn;
            if (btn == null) { continue; }
            btn.onClick.AddListener(() => OnClick_Log(sd.name));
            btn.onClick.AddListener(() => OnClick_TurnCond(sd.audioSource, sd.btn, sd.name));

            // ボタンの名前をnameにする
            Text text;
            text = btn.transform.Find("Text").GetComponent<Text>();
            text.text = sd.name + "(Disable?)";
        }
    }

    private void Setup()
    {
        // 子供にいるボタンを列挙
        Button[] btnList = transform.GetComponentsInChildren<Button>();

        if (SoundList == null) { SoundList = new List<Sound>(); }
        foreach (Button btn in btnList)
        {
            if (SoundList.FindIndex(x => x.btn == btn) < 0)
            {
                SoundList.Add(new Sound(btn));
            }
        }

        // ボタンが無くなっていたらリストから削除
        for (int i = SoundList.Count - 1; i >= 0; i--)
        {
            if (SoundList[i].btn == null)
            {
                SoundList.RemoveAt(i);
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
    private void OnClick_TurnCond(AudioSource @as, Button btn, string name)
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
    [CustomEditor(typeof(UIButtonList))]
    public class UIButtonListEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            UIButtonList btnList = target as UIButtonList;
            if (GUILayout.Button("Setup"))
            {
                btnList.Setup();
            }

            base.OnInspectorGUI();
        }
    }
#endif
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class UIMusicButtonList : MonoBehaviour
{
    [System.Serializable]
    public class MusicChannel
    {
        public string name;
        public Button btn;

        public MusicChannel(Button btn)
        {
            name = "";
            this.btn = btn;
        }
    }

    [SerializeField]
    private List<MusicChannel> MusicList;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene,LoadSceneMode mode)
    {
        foreach (MusicChannel mc in MusicList)
        {
            // イベントを登録
            Button btn = mc.btn;
            if (btn == null) { continue; }
            //btn.onClick.AddListener(() => OnClick_Log(pc.name));
            btn.onClick.AddListener(() => OnClick_SwitchMusic(mc.btn, mc.name));

            // ボタンの名前を変更
            Text text;
            text = btn.transform.Find("Text").GetComponent<Text>();
            text.text = mc.name;
        }
    }

    private void Setup()
    {
        // 子供にいるボタンを列挙
        Button[] btnList = transform.GetComponentsInChildren<Button>();

        if (MusicList == null)
        {
            MusicList = new List<MusicChannel>();
        }
        foreach (Button btn in btnList)
        {
            if (MusicList.FindIndex(x => x.btn == btn) < 0)
            {
                MusicList.Add(new MusicChannel(btn));
            }
        }

        // ボタンが無くなっていたらリストから削除
        for (int i = MusicList.Count - 1; i >= 0; i--)
        {
            if (MusicList[i].btn == null)
            {
                MusicList.RemoveAt(i);
            }
        }
    }

    #region CallBack Function
    private void OnClick_Log(string name)
    {
        Debug.Log(name);
    }
    private void OnClick_SwitchMusic(Button btn, string nextSceneName)
    {
        Text text = btn.transform.Find("Text").GetComponent<Text>();

        if (SceneManager.GetActiveScene().name != nextSceneName)
        {
            SceneManager.LoadScene(nextSceneName);
            text.text = nextSceneName + "(Selected)";
        }
    }
    #endregion

#if UNITY_EDITOR
    [CustomEditor(typeof(UIMusicButtonList))]
    public class UIMusicButtonListEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            UIMusicButtonList btnList = target as UIMusicButtonList;
            if (GUILayout.Button("Setup"))
            {
                btnList.Setup();
            }

            base.OnInspectorGUI();
        }
    }
#endif
}
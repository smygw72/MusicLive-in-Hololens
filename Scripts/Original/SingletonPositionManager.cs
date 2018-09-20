using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonPositionManager : SingletonMonoBehaviour<SingletonPositionManager> {

    static public Vector3 mainCameraPosition;
    static public Vector3 publicSpeakerPosition;

    private void Awake()
    {
        Debug.Log("PositionManager Awake!");
        if (this != Instance)
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void OnSceneLoaded_LoadPublicSpeakerPosition()
    {
        GameObject.Find("Main Camera").transform.position = mainCameraPosition;
        GameObject.Find("PublicSpeaker").transform.position = publicSpeakerPosition;
        Debug.Log("PublicPositionを引き継ぎました");
    }

    private void Update()
    {
        mainCameraPosition = GameObject.Find("Main Camera").transform.position;
        publicSpeakerPosition = GameObject.Find("PublicSpeaker").transform.position;
    }
}

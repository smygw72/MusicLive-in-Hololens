using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonPositionManager : SingletonMonoBehaviour<SingletonPositionManager> {

    static public Vector3 publicSpeakerPosition;

    public void OnSceneLoaded_LoadPublicSpeakerPosition()
    {
        if (publicSpeakerPosition != null)
        {
            GameObject.Find("PublicSpeaker").transform.position = publicSpeakerPosition;
            Debug.Log("PublicPositionを引き継ぎました");
        }
    }

    private void Update()
    {
        publicSpeakerPosition = GameObject.Find("PublicSpeaker").transform.position;
    }

    public void OnSceneUnloaded_SavePublicSpeakerPosition()
    {
        publicSpeakerPosition = GameObject.Find("PublicSpeaker").transform.position;
    }
}

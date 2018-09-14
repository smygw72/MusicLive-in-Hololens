using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePrivateSoundsPosition : MonoBehaviour {

    GameObject publicSound;

	// Use this for initialization
	void Start () {
        publicSound = GameObject.Find("PublicSpeaker");
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = publicSound.transform.position;
	}
}

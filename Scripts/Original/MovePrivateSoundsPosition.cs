using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePrivateSoundsPosition : MonoBehaviour {

    GameObject publicSpeaker;

	// Use this for initialization
	void Start () {
        publicSpeaker = GameObject.Find("PublicSpeaker");
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = publicSpeaker.transform.position;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandOverObject : MonoBehaviour {

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}

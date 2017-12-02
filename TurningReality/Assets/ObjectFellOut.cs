using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFellOut : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}


    private void OnTriggerEnter(Collider p)
    {
        FindObjectOfType<LevelManage>().ForceLoad = true;
    }
}

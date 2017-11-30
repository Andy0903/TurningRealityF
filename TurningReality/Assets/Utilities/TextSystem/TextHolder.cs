using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextHolder : MonoBehaviour {

    public TextAsset textFile;
    public GameObject trigger;
    TextManager parent;

	// Use this for initialization
	void Start () {
        parent = FindObjectOfType<TextManager>();
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other == trigger.GetComponent<Collider>())
            parent.StartNewDialogue(textFile);
    }

    public void ForceEnter()
    {
        parent.StartNewDialogue(textFile);
    }

}

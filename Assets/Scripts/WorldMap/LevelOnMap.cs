using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOnMap : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void playerOver() {
        GetComponent<SpriteRenderer>().color = Color.green;
    }

    public void playerExit() {
        GetComponent<SpriteRenderer>().color = Color.white;
    }

}

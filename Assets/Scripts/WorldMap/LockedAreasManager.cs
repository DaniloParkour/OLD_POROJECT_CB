using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedAreasManager : MonoBehaviour {

	// Use this for initialization
	void Start () {

        //The last level not unlock new area
        for (int i = 0; i < PlayerData.instance._levelsSituation.Length-1; i++) {
            if(PlayerData.instance._levelsSituation[i] == 1) {
                if (transform.Find(("lockedArea"+(i+1))) != null) {
                    //Unlock the area of next level (i+1)
                    transform.Find(("lockedArea" + (i + 1))).gameObject.SetActive(false);
                }
            }
        }

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

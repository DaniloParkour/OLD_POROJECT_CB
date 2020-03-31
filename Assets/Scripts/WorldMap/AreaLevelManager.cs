using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaLevelManager : MonoBehaviour {

    public static AreaLevelManager instance;

    private AreaLevelController[] areasLevel;

    private void Awake() {
        instance = this;
    }

    // Use this for initialization
    void Start () {
        areasLevel = new AreaLevelController[transform.childCount];
        for(int i = 0; i < transform.childCount; i++) {
            areasLevel[i] = transform.GetChild(i).GetComponent<AreaLevelController>();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public string getOneSceneOfLevel(int level) {
        return areasLevel[level - 1].getOneSceneName();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaLevelController : MonoBehaviour {

    [SerializeField]
    private string[] battlescenes;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public string getOneSceneName() {
        return battlescenes[Random.Range(0,battlescenes.Length)];
    }

}

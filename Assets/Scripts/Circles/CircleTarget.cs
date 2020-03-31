using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleTarget : MonoBehaviour {

    private Vector3 initialPosition;

    private Animator anim;

	// Use this for initialization
	void Start () {
        initialPosition = transform.position;
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void backToInitialPosition() {
        transform.position = initialPosition;
    }

    public void selectTargetAnimation() {
        anim.SetTrigger("SelectTarget");
    }

}

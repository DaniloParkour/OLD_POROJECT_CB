using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleMoviments : MonoBehaviour {

    private BubbleController controller;

    private Vector3 speed;
    private Vector3 newSpeed;
    private float timeToChangeDir = 0;

    private Transform targetObject;
    public Transform _targetObject { get { return targetObject; } }

    private float attackVel = 30;
    
    // Use this for initialization
    void Start () {
        speed = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0);
        newSpeed = Vector3.zero;
        controller = GetComponent<BubbleController>();
	}
	
	// Update is called once per frame
	void Update () {

        moveMe();

        timeToChangeDir -= Time.deltaTime;
        if (timeToChangeDir <= 0) {
            if (transform.localPosition.magnitude < 3f)
                newSpeed = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0);
            else
                newSpeed = (transform.parent.position - transform.position).normalized;
            timeToChangeDir = Random.Range(2.0f, 4.0f);
        }

        if (newSpeed != Vector3.zero)
            changeSpeed();

	}

    private void moveMe() {
        if (!controller._selected && !controller.getMouseOver() && targetObject == null)
            transform.Translate(speed * Time.deltaTime);
        else if (targetObject != null)
            transform.Translate((targetObject.position - transform.position).normalized * attackVel * Time.deltaTime);

    }

    private void changeSpeed() {
        speed = Vector3.Lerp(speed, newSpeed, Time.deltaTime);
        if (Vector3.Distance(speed, newSpeed) <= 0.1f)
            newSpeed = Vector3.zero;
    }

    public void goToTarget(Transform target) {
        targetObject = target;
    }

    public void initMe() {
        targetObject = null;
    }

}

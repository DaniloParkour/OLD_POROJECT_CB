using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    [SerializeField]
    private int hitForce = 5;
    
    private Transform target;

    private bool goToTarget = false;

    private Vector3 initialPosition;

    // Use this for initialization
    void Start () {
        initialPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (goToTarget)
            transform.Translate((target.position - transform.position).normalized * 10 * Time.deltaTime);
	}

    public void initBullet(Transform targ, Vector3 pos) {
        target = targ;
        transform.position = pos;
        goToTarget = true;
    }

    public void backToInitialState() {
        transform.position = initialPosition;
        target = null;
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.Equals(target.gameObject)) {
            target.gameObject.SendMessage("hitMe", hitForce);
            backToInitialState();
        }
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMoviment : MonoBehaviour {

    public static BackgroundMoviment instance;

    private SpriteRenderer[] bkgds;
    private float speed = 30;
    private float currentSpeed = 0;

    private bool onMoviment = false;
    public bool _onMoviment { get { return onMoviment; } }

    private float totalSize;

    void Awake() {
        instance = this;
    }

    // Use this for initialization
    void Start () {
        bkgds = new SpriteRenderer[transform.childCount];

        float minX = 999999;
        float maxX = -999999;
        
        for (int i = 0; i < bkgds.Length; i++) {
            bkgds[i] = transform.GetChild(i).GetComponent<SpriteRenderer>();

            if (minX > bkgds[i].transform.position.x - bkgds[i].GetComponent<SpriteRenderer>().size.x / 2)
                minX = bkgds[i].transform.position.x - bkgds[i].GetComponent<SpriteRenderer>().size.x / 2;

            if (maxX < bkgds[i].transform.position.x + bkgds[i].GetComponent<SpriteRenderer>().size.x / 2)
                maxX = bkgds[i].transform.position.x + bkgds[i].GetComponent<SpriteRenderer>().size.x / 2;

        }

        totalSize = maxX - minX;

	}
	
	// Update is called once per frame
	void Update () {
        if (onMoviment) {
            transform.Translate(-Time.deltaTime * currentSpeed, 0, 0);
            if (currentSpeed < speed)
                currentSpeed += Time.deltaTime*30;
        }
	}

    float xMin;
    void LateUpdate() {

        xMin = Camera.main.ScreenToWorldPoint(Vector3.zero).x;
        foreach(SpriteRenderer bg in bkgds)
            if ((bg.transform.position.x + bg.size.x / 2) <= xMin)
                bg.transform.Translate(totalSize, 0, 0);
        
    }

    private void endMoviment() {
        onMoviment = false;
        GameplayManager.instance.gameState = EnumsGame.GameStates.CREATE_BUBBLE;
    }

    public void moveToFront() {
        onMoviment = true;
        GameplayManager.instance.gameState = EnumsGame.GameStates.MOVING_FORWARD;
        currentSpeed = 0;
        Invoke("endMoviment", 5);
    }



}

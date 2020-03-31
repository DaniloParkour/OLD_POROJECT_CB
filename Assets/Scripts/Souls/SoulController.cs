using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulController : MonoBehaviour {

    private Transform target;
    private Vector3 variableVelocity;

    private bool goToTarg = false;
    
	// Use this for initialization
	void Start () {
        //Invoke("goToCamiWand", 2);
	}
	
	// Update is called once per frame
	void Update () {
        if (goToTarg) {
            transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * (15/Vector3.Distance(transform.position, target.position)));
            transform.Translate(variableVelocity * Time.deltaTime);
            if (Vector3.Distance(transform.position, target.position) < 0.6f)
                removeMeFromScene();
        }
	}
    
    private void removeMeFromScene() {

        if (PlayerController.instance != null && 
            target.gameObject.Equals(PlayerController.instance.gameObject.transform.Find("bodyPivot").Find("left_arm").Find("wand").Find("globe").gameObject))
            PlayerController.instance.addQuantSouls();

        //NÃO TEM MAIS OS DOIS TIPOS DE SOUL PARA ADICIONAR E SIM APENAS UM TIPO
        //else if (target.gameObject.Equals(GameplayManager.instance.transform.Find("endLevel")))
          //  PlayerData.instance.addSoul();
          
        goToTarg = false;
        target = null;
        transform.localPosition = Vector3.zero;

        SoulsManager.instance.removeSoulFromScene(this);

    }

    public void goToCamiWand() {
        target = PlayerController.instance.transform.Find("bodyPivot").Find("left_arm").Find("wand").Find("globe");
        variableVelocity = new Vector3(Random.Range(-4f, 2), Random.Range(-4f,6f) ,0);
        goToTarg = true;
    }

    public void goToEndLevel(Transform t) {
        transform.position = PlayerController.instance.transform.Find("bodyPivot").Find("left_arm").Find("wand").Find("globe").position;
        target = t;
        variableVelocity = new Vector3(6, Random.Range(-6f, 6f), 0);
        goToTarg = true;
    }

    public void throwOnDeliveryScene(Transform origin, Transform target) {
        transform.position = origin.position;
        this.target = target;
        variableVelocity = new Vector3(6, Random.Range(-6f, 6f), 0);
        goToTarg = true;
    }

}
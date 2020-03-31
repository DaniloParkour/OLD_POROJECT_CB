using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverySoulManager : MonoBehaviour {

    public GameObject endPanel;
    public Transform bornSoulPosition;
    public Transform targetSouls;

    // Use this for initialization
    void Start () {
        StartCoroutine("deliverySouls");

        /*
        PlayerData p = new PlayerData();
        while(p._totalSouls < 800)
            p.addSoul();
        */

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator deliverySouls() {
        
        yield return new WaitForSeconds(2f);

        Transform targ = transform.Find("endLevel");
        
        Debug.Log("> " + PlayerData.instance._totalSouls + " souls para entregar.");

        while (PlayerData.instance._totalSouls > 0) {

            //Entregar duas almas de uma vez para não demorar muito entregando as almas.
            PlayerData.instance.deliverASoul();
            PlayerData.instance.deliverASoul();

            SoulsManager.instance.throwOnDeliveryScene(bornSoulPosition, targetSouls);

            yield return new WaitForSeconds(0.035f);
        }
        
        yield return new WaitForSeconds(1f);

        endPanel.SetActive(true);
        
    }

    public void loadWorldMap() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("WorldMap");
    }

}

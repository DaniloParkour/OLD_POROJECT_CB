using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulsManager : MonoBehaviour {

    public static SoulsManager instance;

    public SoulController soulPrefab;
    public int quantSouls = 30;

    private List<SoulController> availableSouls;
    private List<SoulController> soulsOnScene;

    private void Awake() {
        instance = this;
    }

    // Use this for initialization
    void Start () {

        availableSouls = new List<SoulController>();
        soulsOnScene = new List<SoulController>();
        SoulController s;
		for(int i = 0; i < quantSouls; i++) {
            s = Instantiate(soulPrefab, transform);
            availableSouls.Add(s);
            s.gameObject.SetActive(false);
        }
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void initSoulsToPlayer(int quant, Vector3 pos) {

        if (quant > availableSouls.Count) {
            SoulController s;
            for (int i = 0; i < quant; i++) {
                s = Instantiate(soulPrefab, transform);
                availableSouls.Add(s);
                s.gameObject.SetActive(false);
            }
        }


        while (quant > 0 && availableSouls.Count > 0) {
            soulsOnScene.Add(availableSouls[0]);
            availableSouls[0].transform.position = pos;
            availableSouls[0].gameObject.SetActive(true);
            availableSouls[0].goToCamiWand();
            availableSouls.Remove(availableSouls[0]);
            quant--;
        }

    }

    public void removeSoulFromScene(SoulController soul) {
        soulsOnScene.Remove(soul);
        availableSouls.Add(soul);
        soul.gameObject.SetActive(false);
    }
    
    public SoulController throwOnDeliveryScene(Transform origin, Transform targ) {
        if (availableSouls.Count > 0) {
            SoulController s = availableSouls[0];
            soulsOnScene.Add(s);
            availableSouls.Remove(s);
            s.gameObject.SetActive(true);
            s.throwOnDeliveryScene(origin, targ);
            return s;
        }
        return null;
    }

    public SoulController deliveryOneSoulToEndLevel(Transform t) {
        if (availableSouls.Count > 0) {
            SoulController s = availableSouls[0];
            soulsOnScene.Add(s);
            availableSouls.Remove(s);
            s.gameObject.SetActive(true);
            s.goToEndLevel(t);
            return s;
        }
        return null;
    }
}

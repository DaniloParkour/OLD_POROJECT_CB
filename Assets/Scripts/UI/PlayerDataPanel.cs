using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDataPanel : MonoBehaviour {

    private Text t_hp, t_lvl, t_exp, t_createBubble, t_create_nBubbles, t_blueBubbleFrequency, t_maxBlueBubbles, t_souls;
    
    void Awake() {
        t_hp = transform.Find("hp").GetComponent<Text>();
        t_lvl = transform.Find("level").GetComponent<Text>();
        t_exp = transform.Find("exp").GetComponent<Text>();
        t_createBubble = transform.Find("timeToCreateBubbles").GetComponent<Text>();
        t_blueBubbleFrequency = transform.Find("blue bubble frequency").GetComponent<Text>();
        t_create_nBubbles = transform.Find("creates n bubbles").GetComponent<Text>();
        t_maxBlueBubbles = transform.Find("max blue bubbles").GetComponent<Text>();
        t_souls = transform.Find("souls").GetComponent<Text>();

    }

    // Use this for initialization
    void Start() {
        gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void openMe() {
        t_hp.text = "HP = " + PlayerData.instance.hpMax();
        t_lvl.text = "Level = " + PlayerData.instance.getPlayerLevel();
        t_exp.text = "Exp = " + PlayerData.instance._totalExp;
        t_createBubble.text = "Demora " + PlayerData.instance.getTimeToCreateBubble() + "s p/ criar bolhas.";
        t_create_nBubbles.text = "Cria = " + PlayerData.instance.quantBolhas() + " bolhas por vez.";
        t_blueBubbleFrequency.text = "1 bolha azul criada a cada " + PlayerData.instance.waterBubbleFrequency() + " criações.";
        t_maxBlueBubbles.text = "No máximo = " + PlayerData.instance.maxWaterBubblesOnScene() + " bolhas azuis na cena.";
        t_souls.text = "Souls = " + PlayerData.instance._totalSouls;
        gameObject.SetActive(true);
    }

}

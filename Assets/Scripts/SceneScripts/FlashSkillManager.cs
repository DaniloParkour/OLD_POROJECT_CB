using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashSkillManager : MonoBehaviour {

    public static FlashSkillManager instance;

    [SerializeField]
    private float timeToFlash = 0.2f;

    private Image flashPanel;
    private float currentTimeOnFlash = -10;
    
	// Use this for initialization
	void Start () {
        flashPanel = GetComponent<Image>();
        flashPanel.color = new Color(flashPanel.color.r, flashPanel.color.g, flashPanel.color.b, 0);
        flashPanel.enabled = false;
        currentTimeOnFlash = -10;

        instance = this;
    }
	
	// Update is called once per frame
	void Update () {
        if (currentTimeOnFlash != -10) {
            flashPanel.color = new Color(flashPanel.color.r, flashPanel.color.g, flashPanel.color.b, 
                currentTimeOnFlash/timeToFlash);
            currentTimeOnFlash -= Time.deltaTime;
            if(currentTimeOnFlash <= 0) {
                flashPanel.color = new Color(flashPanel.color.r, flashPanel.color.g, flashPanel.color.b, 0);
                flashPanel.enabled = false;
                currentTimeOnFlash = -10;
            }
        }
	}

    public void initFlash(Color cor) {
        flashPanel.enabled = true;
        flashPanel.color = cor;
        currentTimeOnFlash = timeToFlash;
    }

}

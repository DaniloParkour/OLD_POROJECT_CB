using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleInputs : MonoBehaviour {

    private BubbleController controller;

    private bool mouseOver;
    public bool _mouseOver { get { return mouseOver; } }
    //private bool mouseDown;

    private Vector3 initialSize;
    private Vector3 mouseOverSize;

    private Rigidbody2D rgdb;

    private bool holding = false;

    // Use this for initialization
    void Start () {
        initialSize = transform.localScale;
        mouseOverSize = initialSize * 1.15f;
        controller = GetComponent<BubbleController>();
        rgdb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseEnter() {

        mouseOver = true;
        
        if (!controller._selected) {
            transform.localScale = mouseOverSize;
            rgdb.isKinematic = true;
        }
    }

    void OnMouseExit() {

        mouseOver = false;

        if (!controller._selected) {
            transform.localScale = initialSize;
            rgdb.isKinematic = false;
        }
    }

    void OnMouseDown() {
        controller.selectMe();
        holding = true;
    }

    void OnMouseUp() {

        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero,
                Mathf.Infinity, LayerMask.GetMask("Bubble"));

        if (hit.collider != null && !hit.collider.gameObject.Equals(this.gameObject)) {
            initFlashSkill(hit.collider.GetComponent<BubbleController>());
        } else {
            if (BubblesManager.instance._selectedBubble != null &&
                EnemiesManager.instance._enemyOnTarget != null &&
                !PlayerController.instance._creatingBubble) {

                BubblesManager.instance._selectedBubble.atackEnemy(EnemiesManager.instance._enemyOnTarget);
            }
        }
        
        holding = false;

        controller.removeSelect();
        
    }

    private void initFlashSkill(BubbleController otherBubble) {
        //For each skill disabled, test if the flash skill should actived
        //POISON -> Blue + YELLOW
        if (PlayerData.instance.getSkillLevel(EnumsGame.GameSkills.POISON) > 0) {
            if (controller.collor.Equals(EnumsGame.BubbleCollors.YELLOW) && otherBubble.collor.Equals(EnumsGame.BubbleCollors.BLUE)
                || controller.collor.Equals(EnumsGame.BubbleCollors.BLUE) && otherBubble.collor.Equals(EnumsGame.BubbleCollors.YELLOW)) {
                controller.goMix(otherBubble);
            }
        }
        //EARTQUAKE -> YELLOW + RED
        if (PlayerData.instance.getSkillLevel(EnumsGame.GameSkills.EARTHQUAKE) > 0) {
            if (controller.collor.Equals(EnumsGame.BubbleCollors.YELLOW) && otherBubble.collor.Equals(EnumsGame.BubbleCollors.RED)
                || controller.collor.Equals(EnumsGame.BubbleCollors.RED) && otherBubble.collor.Equals(EnumsGame.BubbleCollors.YELLOW)) {
                controller.goMix(otherBubble);
            }
        }
        //ARKANE -> RED + BLUE
        if (PlayerData.instance.getSkillLevel(EnumsGame.GameSkills.ARKANE) > 0) {
            if (controller.collor.Equals(EnumsGame.BubbleCollors.RED) && otherBubble.collor.Equals(EnumsGame.BubbleCollors.BLUE)
                || controller.collor.Equals(EnumsGame.BubbleCollors.BLUE) && otherBubble.collor.Equals(EnumsGame.BubbleCollors.RED)) {
                controller.goMix(otherBubble);
            }
        }
    }

}

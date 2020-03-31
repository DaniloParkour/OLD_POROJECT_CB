using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour {

    private Animator anim;
    private string[] animaNames;
    private string[] parameterNames;

    private bool onCreateBubbleAnim = false;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();

        animaNames = new string[6];
        animaNames[0] = "CamiIddle";
        animaNames[1] = "CamiBasicSkill";
        animaNames[2] = "CreateBubbles";
        animaNames[3] = "";
        animaNames[4] = "";
        animaNames[5] = "";

        parameterNames = new string[6];
        parameterNames[0] = "CreateBubble"; //is a Trigger
        parameterNames[1] = "";
        parameterNames[2] = "";
        parameterNames[3] = "";
        parameterNames[4] = "";
        parameterNames[5] = "";
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void playAnimation(EnumsGame.PlayerAnimations animPlayer) {
        //Debug.Log("TA NO PLAY ANIMATION!");
        if (animPlayer.Equals(EnumsGame.PlayerAnimations.CREATE_BUBBLE)) {
            if (!onCreateBubbleAnim) {
                anim.SetTrigger(parameterNames[0]);
                onCreateBubbleAnim = true;
            }
        }
    }

    private void finishCreateBubbleAnim() {
        onCreateBubbleAnim = false;
    }

}

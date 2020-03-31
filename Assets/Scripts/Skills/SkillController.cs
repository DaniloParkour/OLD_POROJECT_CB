using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour {

    public EnumsGame.GameSkills skill;
    
    private Vector3 initialPos;
    private Vector3 initialScale;

    private Animator anim;

    private float duration = 5;
    public float _duration { get { return duration; } set { duration = value; } }

    private EnemiesManager enemyTarget;

	// Use this for initialization
	void Start () {
        initialPos = transform.position;
        initialScale = transform.localScale;
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (skill.Equals(EnumsGame.GameSkills.ELETRIC_BASIC)) {
            duration -= Time.deltaTime;
            if (duration <= 0)
                removeFromScreen();
        }
	}

    public void initMe(Vector3 pos, float size) {
        transform.position = pos;
        transform.localScale = initialScale*size;

        if (skill.Equals(EnumsGame.GameSkills.ELETRIC_BASIC))
            duration = PlayerController.instance._eletricDuration;

        anim.Rebind();
    }

    private void removeFromScreen() {
        transform.position = initialPos;
        transform.localScale = initialScale;
    }
    


}

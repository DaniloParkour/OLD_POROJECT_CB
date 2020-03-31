using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoving : MonoBehaviour {

    public EnumsGame.EnemyType type;

    [SerializeField]
    private float velocityWalking = 0.2f;
    [SerializeField]
    private float durationWalking = 2;
    [SerializeField]
    private float attackRange = 2;
    
    private float timeOfSkill;
    private float currentDistance;
    private float currentTimeOnSkill = -10;

    private EnemyController controller;
    private EnemyUseSkill skill;
    

	// Use this for initialization
	void Start () {
        controller = GetComponent<EnemyController>();
        skill = GetComponent<EnemyUseSkill>();
	}
	
	// Update is called once per frame
	void Update () {
        if(currentTimeOnSkill != -10) {
            useSkill();
        }
	}

    private void useSkill() {
        
        if (Vector3.Distance(PlayerController.instance.transform.position, transform.position) <= attackRange) {
            PlayerController.instance.hitMe(controller._hitForce);
            currentTimeOnSkill = -10;
            controller.resetTimeToAttack();
        } else {
            currentTimeOnSkill -= Time.deltaTime;

            transform.Translate(-velocityWalking * Time.deltaTime, 0, 0);

            //Debug.Log("Veio AQUI!");

            if (currentTimeOnSkill <= 0) {
                currentTimeOnSkill = -10;
                controller.resetTimeToAttack();

                if (type.Equals(EnumsGame.EnemyType.WIZARD))
                    skill.useSkill();

            }
        }
        
    }

    public void useAttack() {
        if (velocityWalking >= 0) {
            currentTimeOnSkill = durationWalking;
        }
    }
    
}

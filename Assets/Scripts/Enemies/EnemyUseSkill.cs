using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUseSkill : MonoBehaviour {

    public EnumsGame.GameBullets bulletType;

    private EnemyController controller;

	// Use this for initialization
	void Start () {
        controller = GetComponent<EnemyController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void useSkill() {

        if (bulletType.Equals(EnumsGame.GameBullets.ENEMY_BULLET_01)) {
            SkillsManager.instance.useEnemyBullet(bulletType, transform.position);
        }

    }

}

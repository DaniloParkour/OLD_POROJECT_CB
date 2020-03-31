using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsManager : MonoBehaviour {

    public static SkillsManager instance;
    
    private SkillController fireBasic;
    private SkillController eletricBasic;
    private SkillController waterBasic;

    private BulletController enemyBullet01;

    private SkillController choosedSkill;

    void Awake() {
        instance = this;
    }

    // Use this for initialization
    void Start () {
        fireBasic = transform.Find("fireBasicSkill").GetComponent<SkillController>();
        eletricBasic = transform.Find("eletricBasicSkill").GetComponent<SkillController>();
        waterBasic = transform.Find("waterBasicSkill").GetComponent<SkillController>();

        enemyBullet01 = transform.Find("enemyBullet01").GetComponent<BulletController>();
    }
    
	// Update is called once per frame
	void Update () {
		
	}

    private void initSkill(EnumsGame.GameSkills skill, Vector3 pos, float size) {

        if (skill.Equals(EnumsGame.GameSkills.FIRE_BASIC))
            fireBasic.initMe(pos, size);
        else if (skill.Equals(EnumsGame.GameSkills.ELETRIC_BASIC))
            eletricBasic.initMe(pos, size);
        else if (skill.Equals(EnumsGame.GameSkills.WATER_BASIC))
            waterBasic.initMe(pos, size);

    }
    
    public void useSkill(EnumsGame.BubbleCollors collor, Vector3 pos, float size) {
        if (collor.Equals(EnumsGame.BubbleCollors.RED))
            initSkill(EnumsGame.GameSkills.FIRE_BASIC, pos, size);
        else if (collor.Equals(EnumsGame.BubbleCollors.YELLOW))
            initSkill(EnumsGame.GameSkills.ELETRIC_BASIC, pos, size);
        else if (collor.Equals(EnumsGame.BubbleCollors.BLUE))
            initSkill(EnumsGame.GameSkills.WATER_BASIC, pos, size);
    }

    public void useEnemyBullet(EnumsGame.GameBullets bullet, Vector3 pos) {
        if (bullet.Equals(EnumsGame.GameBullets.ENEMY_BULLET_01)) {
            enemyBullet01.gameObject.SetActive(true);
            enemyBullet01.initBullet(PlayerController.instance.transform, pos);
        }
    }

}

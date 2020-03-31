using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBuffDebuff : MonoBehaviour {

    private bool eletricDebuff = false;
    private bool poisonDebuff = false;

    private int eletricDebuffHit = 0;
    private int poisonDebuffHit = 0;

    private float timeToEletricDebuff = 0;
    private float timeToPoisonDebuff = 0;

    private float currentTimeOnEletric = 0;
    private float currentTimeOnPoison = 0;

    private EnemyController controller;

    // Use this for initialization
    void Start () {
        controller = GetComponent<EnemyController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void hitWithPoison() {
        controller.hitMe(poisonDebuffHit);
        Invoke("hitWithPoison", timeToPoisonDebuff);
    }

    public void addPoisonDebuff() {
        poisonDebuff = true;
        poisonDebuffHit = PlayerData.instance._eletricBase; //O poison é calculado com base no nivel da eletricidade.

        timeToPoisonDebuff = 2;

        if (PlayerData.instance.getSkillLevel(EnumsGame.GameSkills.POISON) == 2)
            timeToPoisonDebuff = 1.8f;
        else if (PlayerData.instance.getSkillLevel(EnumsGame.GameSkills.POISON) == 3)
            timeToPoisonDebuff = 1.6f;
        else if (PlayerData.instance.getSkillLevel(EnumsGame.GameSkills.POISON) == 4) {
            timeToPoisonDebuff = 2.2f;
            poisonDebuffHit = PlayerData.instance._eletricBase + 1;
        } else if (PlayerData.instance.getSkillLevel(EnumsGame.GameSkills.POISON) == 5) {
            timeToPoisonDebuff = 2f;
            poisonDebuffHit = PlayerData.instance._eletricBase + 1;
        } else if (PlayerData.instance.getSkillLevel(EnumsGame.GameSkills.POISON) == 6) {
            timeToPoisonDebuff = 1.8f;
            poisonDebuffHit = PlayerData.instance._eletricBase + 1;
        } else if (PlayerData.instance.getSkillLevel(EnumsGame.GameSkills.POISON) == 7) {
            timeToPoisonDebuff = 2.4f;
            poisonDebuffHit = PlayerData.instance._eletricBase + 2;
        } else if (PlayerData.instance.getSkillLevel(EnumsGame.GameSkills.POISON) == 8) {
            timeToPoisonDebuff = 2.2f;
            poisonDebuffHit = PlayerData.instance._eletricBase + 2;
        } else if (PlayerData.instance.getSkillLevel(EnumsGame.GameSkills.POISON) == 9) {
            timeToPoisonDebuff = 2f;
            poisonDebuffHit = PlayerData.instance._eletricBase + 2;
        } else if (PlayerData.instance.getSkillLevel(EnumsGame.GameSkills.POISON) == 10) {
            timeToPoisonDebuff = 2.2f;
            poisonDebuffHit = PlayerData.instance._eletricBase + 3;
        }

        poisonDebuffHit *= 3;

        hitWithPoison();

    }

}

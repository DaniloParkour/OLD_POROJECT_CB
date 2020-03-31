using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour {

    [SerializeField]
    private int hp;
    [SerializeField]
    private int hitForce = 10;
    public int _hitForce { get { return hitForce; } set { hitForce = value; } }
    
    private int currentHp;

    private Image hpImg;
    private Image attackBarImg;

    private bool mouseOver = false;

    private Transform posCircleGround;

    [SerializeField]
    private float timeToAttack;
    public float _timeToAttack { get { return timeToAttack; } }

    private float currentTimeToAttack;

    private bool chargeSkill = true;

    private EnemyMoving moving;

    [SerializeField]
    private int quantSouls = 5;

    private bool waterBasicEffect = false;

    // Use this for initialization
    void Start () {
        currentHp = hp;
        hpImg = transform.Find("bkg_hp").Find("hp").GetComponent<Image>();
        posCircleGround = transform.Find("circle_ground");

        currentTimeToAttack = 0;
        attackBarImg = transform.Find("useSkill").GetComponent<Image>();

        moving = GetComponent<EnemyMoving>();

    }
	
	// Update is called once per frame
	void Update () {

        if(chargeSkill)
            chargeSkillBar();

	}

    private void chargeSkillBar() {
        attackBarImg.fillAmount = currentTimeToAttack / timeToAttack;

        if (currentTimeToAttack < timeToAttack) {
            if(!waterBasicEffect)
                currentTimeToAttack += Time.deltaTime;
            else
                currentTimeToAttack -= Time.deltaTime;

            if(waterBasicEffect && currentTimeToAttack <= 0) {
                waterBasicEffect = false;
                attackBarImg.color = Color.red;
            }

            if (currentTimeToAttack >= timeToAttack) {
                currentTimeToAttack = timeToAttack;
                moving.useAttack();
            }
        }

    }
    
    void OnMouseOver() {

        //if(BubblesManager.instance._selectedBubble != null) {
        if (!mouseOver) {
            GameplayManager.instance._circleTarget.transform.position = transform.position + Vector3.up / 2;
            mouseOver = true;
        }
        //}
        
    }

    void OnMouseExit() {
        /*if (BubblesManager.instance._selectedBubble != null) {
            GameplayManager.instance._circleTarget.backToInitialPosition();
            EnemiesManager.instance._enemyOnTarget = null;
        }*/
        GameplayManager.instance._circleTarget.backToInitialPosition();
        mouseOver = false;
    }
    
    private void OnMouseUp() {
        if (mouseOver) {
            EnemiesManager.instance._enemyOnTarget = this;
            GameplayManager.instance._circleSelected.transform.position = posCircleGround.position;
        }
    }

    public void hitMe(int value) {
        currentHp -= value;
        if (currentHp < 0)
            currentHp = 0;
        hpImg.fillAmount = ((float)currentHp/ (float)hp);
        //Debug.Log("Fill = "+ hpImg.fillAmount);
        if (currentHp == 0)
            killMe();
    }

    private void killMe() {
        if (EnemiesManager.instance._enemyOnTarget != null && EnemiesManager.instance._enemyOnTarget.Equals(this)) {
            EnemiesManager.instance._enemyOnTarget = null;
            GameplayManager.instance._circleSelected.backToInitialPosition();
        }

        SoulsManager.instance.initSoulsToPlayer(quantSouls, transform.position);

        transform.parent.GetComponent<EnemiesManager>().removeEnemyFromScene(this);
    }

    public void resetTimeToAttack() {
        currentTimeToAttack = 0;
    }

    public void addWaterBasicEffect() {
        waterBasicEffect = true;
        attackBarImg.color = Color.blue;
    }
    
}

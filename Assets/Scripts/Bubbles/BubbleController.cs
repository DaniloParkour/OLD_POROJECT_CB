using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleController : MonoBehaviour {

    public EnumsGame.BubbleCollors collor;

    private bool attacking = false;
    public bool _attacking { get { return attacking; } }

    private bool selected = false;
    public bool _selected { get { return selected; } }  //set { selected = value; GetComponent<Animator>().SetBool("selected", selected); if (selected) vel = Vector2.zero; } }

    private bool used = false;
    public bool _used { get { return used; } }

    private Vector3 initialScale;

    private Rigidbody2D rgdb;
    private Animator anim;
    
    /*Usar se fofr preciso na nova mecanica do controle do mouse*/
    //private Vector3 posMouseDown;
    //private Vector3 posMouseUp;

    private bool holding = false;
    private float energy = 1;
    private Vector3 v = Vector3.zero;
    private bool toNextTurn = false;

    private BubbleMoviments moviments;
    private BubbleInputs inputs;

    private Color currentColor;

    void Awake() {
        initialScale = transform.localScale;
        moviments = GetComponent<BubbleMoviments>();
        inputs = GetComponent<BubbleInputs>();
        anim = GetComponent<Animator>();
    }


    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        
    }
    
    public bool getMouseOver() {
        return inputs._mouseOver;
    }

    public void selectMe() {
        selected = true;
        anim.SetBool("selected", true);
        BubblesManager.instance.setSelectedBubble(this);
    }

    public void removeSelect() {
        selected = false;
        anim.SetBool("selected", false);
        BubblesManager.instance.setSelectedBubble(null);
    }

    public void atackEnemy(EnemyController enemy) {
        if (enemy == null)
            return;
        attacking = true;
        GameplayManager.instance._circleTarget.selectTargetAnimation();
        moviments.goToTarget(enemy.transform);
    }

    public void goMix(BubbleController otherBubble) {
        moviments.goToTarget(otherBubble.transform);
    }

    public void initMe(EnumsGame.BubbleCollors cor, Vector3 pos) {
        collor = cor;
        transform.position = pos;
        currentColor = Color.red;
        if (cor.Equals(EnumsGame.BubbleCollors.YELLOW))
            currentColor = Color.yellow;
        else if (cor.Equals(EnumsGame.BubbleCollors.BLUE))
            currentColor = Color.blue;

        GetComponent<SpriteRenderer>().color = currentColor;
        transform.Find("b_ring").GetComponent<SpriteRenderer>().color = currentColor;
        transform.Find("b_bright").GetComponent<SpriteRenderer>().color = currentColor;

        attacking = false;
        used = false;
        moviments.initMe();

    }

    private void useMe() {
        BubblesManager.instance.removeBubbleFromScene(this);
        if (collor.Equals(EnumsGame.BubbleCollors.YELLOW))
            StopCoroutine("eletricBubbleHits");
    }

    private void useMeOnEnemy(Transform enemy) {
        SkillsManager.instance.useSkill(collor, enemy.position, 1);

        if (collor.Equals(EnumsGame.BubbleCollors.YELLOW)) {
            StartCoroutine("eletricBubbleHits");
            Invoke("useMe", PlayerController.instance._eletricDuration);
        } else  if (collor.Equals(EnumsGame.BubbleCollors.RED)) {
            enemy.GetComponent<EnemyController>().hitMe((int)PlayerController.instance._playerPower*6);
            useMe();
        } else {

            /*Adicionar depois conteúdo que a bolha da água irá influenciar no jogo!!!*/
            waterBasicSkill();

            useMe();
        }

    }

    public void allowsTheUse() {
        used = false;
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (!used && moviments._targetObject != null) {
            if (moviments._targetObject.Equals(collision.gameObject.transform)
              && collision.gameObject.layer == LayerMask.NameToLayer("Bubble")) {
                initFlashSkill(collision.gameObject.GetComponent<BubbleController>());
                used = true;
            }
        }
    }

    void OnTriggerStay2D(Collider2D collision) {

        if (!used && moviments._targetObject != null) {
            
            if (attacking && collision.gameObject.layer == LayerMask.NameToLayer("Enemy") &&
                collision.gameObject.Equals(moviments._targetObject.gameObject)) {

                useMeOnEnemy(collision.gameObject.transform);

                used = true;
                PlayerController.instance.useOneBubble();

            }
        }
    }

    private void initFlashSkill(BubbleController othetBubble) {
        if (collor.Equals(EnumsGame.BubbleCollors.RED) && othetBubble.collor.Equals(EnumsGame.BubbleCollors.YELLOW) ||
            collor.Equals(EnumsGame.BubbleCollors.YELLOW) && othetBubble.collor.Equals(EnumsGame.BubbleCollors.RED)) {
            FlashSkillManager.instance.initFlash(new Color(1, 0.6f, 0, 1));
            EnemiesManager.instance.initFlashEarthQuake();
            othetBubble.useMe();
            useMe();
            PlayerController.instance.useOneBubble();
            PlayerController.instance.useOneBubble();
        }
        else if (collor.Equals(EnumsGame.BubbleCollors.BLUE) && othetBubble.collor.Equals(EnumsGame.BubbleCollors.YELLOW) ||
         collor.Equals(EnumsGame.BubbleCollors.YELLOW) && othetBubble.collor.Equals(EnumsGame.BubbleCollors.BLUE)) {
            FlashSkillManager.instance.initFlash(Color.green);
            EnemiesManager.instance.initFlashPoison();
            othetBubble.useMe();
            useMe();
            PlayerController.instance.useOneBubble();
            PlayerController.instance.useOneBubble();
        } else if (collor.Equals(EnumsGame.BubbleCollors.RED) && othetBubble.collor.Equals(EnumsGame.BubbleCollors.BLUE) ||
         collor.Equals(EnumsGame.BubbleCollors.BLUE) && othetBubble.collor.Equals(EnumsGame.BubbleCollors.RED)) {
            FlashSkillManager.instance.initFlash(new Color(1, 0, 0.6f, 1));
            EnemiesManager.instance.initFlashArkane();
            othetBubble.useMe();
            useMe();
            PlayerController.instance.useOneBubble();
            PlayerController.instance.useOneBubble();
        }
    }

    private void waterBasicSkill() {
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, 2, LayerMask.GetMask("Enemy"));
        if(cols.Length > 0) {
            foreach(Collider2D c in cols) {
                c.GetComponent<EnemyController>().addWaterBasicEffect();
            }
        }
    }
    
    IEnumerator eletricBubbleHits() {
        EnemyController enemy = moviments._targetObject.GetComponent<EnemyController>();
        while (enemy != null) {
            enemy.hitMe((int)PlayerController.instance._playerPower);
            yield return new WaitForSeconds(0.5f);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCreateBubble : MonoBehaviour {

    [SerializeField]
    private float timeToBubble;
    [SerializeField]
    private int maxBubbles;

    private float currentTimeToCreate;

    //private int quantBubblesNextTurn = 0;

    private bool enableCreate = false;
    public bool _enableCreate { get { return enableCreate; } }

    private Transform localCreate;

    private BubbleController newBubble;

    private EnumsGame.BubbleCollors[] collors;
    private int randInt = 0;
    private int colorCount = 0;

    private int quantNotUsed = 0;
    
    private Image attackBarImg;
    
    // Use this for initialization
    void Start () {

        timeToBubble = PlayerData.instance.getTimeToCreateBubble();
        maxBubbles = PlayerData.instance.quantBolhas();

        currentTimeToCreate = timeToBubble;
        localCreate = transform.Find("createBubbles");
        collors = new EnumsGame.BubbleCollors[2];
        collors[0] = EnumsGame.BubbleCollors.RED;
        collors[1] = EnumsGame.BubbleCollors.YELLOW;

        attackBarImg = transform.Find("useSkill").GetComponent<Image>();
        
    }
	
	// Update is called once per frame
	void Update () {
        if (GameplayManager.instance.gameState.Equals(EnumsGame.GameStates.CREATE_BUBBLE)) {
            currentTimeToCreate -= Time.deltaTime;
            //Debug.Log("TimeToCreate = "+currentTimeToCreate);
            if(currentTimeToCreate < 0.2f)
                verifyCreateBubble();
        } else if (GameplayManager.instance.gameState.Equals(EnumsGame.GameStates.USE_BUBBLES)) {

            //if (BubblesManager.instance.quantBubblesOnGame() <= quantBubblesNextTurn) { //QuantBubblesOnGame never should be smaller
            if (quantNotUsed <= 0) { //QuantBubblesOnGame never should be smaller
                GameplayManager.instance.gameState = EnumsGame.GameStates.CREATE_BUBBLE;
            }
        }

        if (PlayerController.instance._changeSkill)
            chargeSkillBar();

    }

    /* Checks when it can create bubbkes and creates at correct time.*/
    private void verifyCreateBubble() {
        PlayerController.instance.playAnimation(EnumsGame.PlayerAnimations.CREATE_BUBBLE);

        //if (currentTimeToCreate <= 0 && BubblesManager.instance.quantBubblesOnGame() + quantBubblesNextTurn < maxBubbles && enableCreate) {
        if (currentTimeToCreate <= 0 && quantNotUsed < maxBubbles && enableCreate) {
            
            if (currentTimeToCreate <= 0) {

                for(int i = 0; i < maxBubbles; i++) {
                    newBubble = BubblesManager.instance.getABubble();
                    quantNotUsed++;
                
                    newBubble.initMe(collors[colorCount], localCreate.position);
                    colorCount = (colorCount + 1) % collors.Length;

                    randInt = Random.Range(1, 101);
                
                    if (randInt > PlayerData.instance.waterCreaterand() && 
                        BubblesManager.instance.quantBubblesType(EnumsGame.BubbleCollors.BLUE) < PlayerData.instance.maxWaterBubblesOnScene()) {
                        newBubble = BubblesManager.instance.getABubble();
                        newBubble.initMe(EnumsGame.BubbleCollors.BLUE, localCreate.position);
                    }
                
                    currentTimeToCreate = 0.2f;
                    PlayerController.instance._creatingBubble = true;
                }

            }

            //if (quantBubblesNextTurn + BubblesManager.instance.quantBubblesOnGame() >= maxBubbles) {
            if (quantNotUsed >= maxBubbles) {

                //Debug.Log("Vai Usar Agora!");
                currentTimeToCreate = timeToBubble;
                GameplayManager.instance.gameState = EnumsGame.GameStates.USE_BUBBLES;
                enableCreate = false;
                PlayerController.instance._creatingBubble = false;
                
                //quantBubblesNextTurn = 0;
                
            }
        }
        
    }

    /*Allow the creation of the bubbles at the correct moment of the animation*/
    private void enableCreateNow() {
        enableCreate = true;
    }

    
    private void chargeSkillBar() {
        attackBarImg.fillAmount = Mathf.Abs((currentTimeToCreate / timeToBubble) - 1);
    }


    public void useOneBubble() {
        if(quantNotUsed > 0)
        quantNotUsed--;
    }

    public void resetCreation() {
        //quantBubblesNextTurn = 0;
        quantNotUsed = 0;
    }
    
}

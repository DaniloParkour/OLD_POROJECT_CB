using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour {

    public static GameplayManager instance;

    public EnumsGame.GameplayType gameType = EnumsGame.GameplayType.FIGHT_GAME;

    public EnumsGame.GameStates gameState;

    public GameObject canvasCamera;
    
    [SerializeField]
    private int quantAdvance;

    [SerializeField]
    private CircleTarget circleTarger;
    public CircleTarget _circleTarget { get { return circleTarger; } }

    [SerializeField]
    private CircleTarget circleSelected;
    public CircleTarget _circleSelected { get { return circleSelected; } }

    [SerializeField]
    private int level;
    public int _level { get { return level; } }

    private GameObject winPanel;
    private bool waitForCallEnemies;

    //Para evitar chamado infinitamente a finalização do level.
    private bool finishedLevel = false;

    private void Awake() {
        instance = this;
        new PlayerData();
    }

    // Use this for initialization
    void Start() {
        gameState = EnumsGame.GameStates.INIT_LEVEL;

        //remover depois
        Invoke("initGame", 0);

        winPanel = canvasCamera.transform.Find("PlayerWin").gameObject;
        finishedLevel = false;
        
    }

    // Update is called once per frame
    void Update() {

        if (gameType.Equals(EnumsGame.GameplayType.LEVEL_GAME) && !BackgroundMoviment.instance._onMoviment && waitForCallEnemies) {
            EnemiesManager.instance.initNextEnemies();
            waitForCallEnemies = false;
        }

    }

    private void initGame() {
        gameState = EnumsGame.GameStates.CREATE_BUBBLE;
    }
    
    public void openWinPanel() {
        winPanel.gameObject.SetActive(true);
    }

    public void loadWorldMapScene() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("WorldMap");
    }

    public void advanceOnLevel() {
        
        if (finishedLevel)
            return;

        if (gameType.Equals(EnumsGame.GameplayType.LEVEL_GAME)) {

            if (BackgroundMoviment.instance._onMoviment)
                return;
        
            if (quantAdvance > 0) {
                PlayerController.instance.initAdvance();
                BackgroundMoviment.instance.moveToFront();
                BubblesManager.instance.explodesAllBubbles();
                waitForCallEnemies = true;
                quantAdvance--;
            } else {
                StartCoroutine("deliverySouls");
                finishedLevel = true;
            }
        }

    }

    private bool onDelivery = false;
    IEnumerator deliverySouls() {

        onDelivery = true;

        yield return new WaitForSeconds(1f);

        Transform targ = transform.Find("endLevel");

        float tm = 0;

        Debug.Log("> " + PlayerData.instance._totalSouls + " souls para entregar.");

        while (PlayerData.instance._totalSouls > 0) {

            //Entregar duas almas de uma vez para não demorar muito entregando as almas.
            PlayerData.instance.deliverASoul();
            PlayerData.instance.deliverASoul();

            SoulsManager.instance.deliveryOneSoulToEndLevel(targ);

            yield return new WaitForSeconds(0.035f);
            tm += Time.deltaTime;
        }

        Debug.Log("> "+tm+" <==> Salvar novo total de almas do jogador.");

        yield return new WaitForSeconds(1f);

        onDelivery = false;
        openWinPanel();

        PlayerData.instance._levelsSituation[level] = 1;

    }

}

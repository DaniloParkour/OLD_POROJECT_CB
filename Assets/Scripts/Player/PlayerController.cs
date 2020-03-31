using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    public static PlayerController instance;

    [SerializeField]
    private int hp;

    private PlayerAnimations anima;
    private PlayerCreateBubble createBubble;
    
    private float eletricDuration = 8;
    public float _eletricDuration { get { return eletricDuration; } }

    private float playerPower = 1f;
    public float _playerPower { get { return playerPower; } }

    private int currentHp;

    private Image hpImg;
    
    private bool chargeSkill = true;
    public bool _changeSkill { get { return chargeSkill; } }

    private bool creatingBubble = false;
    public bool _creatingBubble { get { return creatingBubble; } set { creatingBubble = value; } }

    //Data to current gameplay
    private int currentLevel;
    private int _currentLevel { get { return currentLevel; } set { currentLevel = value; } }

    void Awake() {
        instance = this;
    }

    // Use this for initialization
    void Start () {
        anima = GetComponent<PlayerAnimations>();
        createBubble = GetComponent<PlayerCreateBubble>();
        hp = PlayerData.instance.hpMax();

        currentHp = hp;
        hpImg = transform.Find("bkg_hp").Find("hp").GetComponent<Image>();
        
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void addQuantSouls() {
        PlayerData.instance.addSoul();
    }

    public void playAnimation(EnumsGame.PlayerAnimations animPlayer) {
        anima.playAnimation(animPlayer);
    }

    public void useOneBubble() {
        createBubble.useOneBubble();
    }

    public void hitMe(int hitValue) {
        currentHp -= hitValue;
        hpImg.fillAmount = ((float)currentHp/hp);
        if (currentHp < 0)
            reloadSceneTest();
    }

    public void reloadSceneTest() {
        Debug.Log("Implementar punição por derrota!");
        SceneManager.LoadScene("WorldMap");
    }

    public void initAdvance() {
        Debug.Log("Player Avança! Iniciar Animação! Parar criação de bolhas com o ENUM GameState.");
        createBubble.resetCreation();
    }
}

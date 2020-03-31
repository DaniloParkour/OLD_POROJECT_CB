using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnWorldMap : MonoBehaviour {

    public static PlayerOnWorldMap instance;

    [SerializeField]
    private string[] levelsNames;

    private int selectedLevel = -1;
    
    private float moveX , moveY;

    private Vector3 anteriorPosOnWalkingArea;

    private Animator anim;

    //0 standing, 1 up, 2 right, 3 down, 4 left
    private int direction = 0;

    /*
     Quando o jogo for carregado pelos dados do computador. Setar a posição do jogador na hora do save.
    */
    private GameObject onArea;
    public GameObject _onArea { get { return onArea; } }
    
    void Awake() {
        instance = this;
        if (PlayerData.instance != null) {
            transform.position = PlayerData.instance._posPlayerOnMap;
            Debug.Log("Player pra posição dele! "+PlayerData.instance._posPlayerOnMap);
        } else {
            new PlayerData();
        }
        
    }

    // Use this for initialization
    void Start () {
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);
        anim = transform.Find("PlayerOnMap").GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        playerMoviments();
        PlayerData.instance._posPlayerOnMap = transform.position;

        if (Input.GetKeyUp(KeyCode.Space))
            loadGameLevel();
    }

    private string str_dir = "direction";
    private string str_horiz = "Horizontal";
    private string str_vertic = "Vertical";
    private void playerMoviments() {
        moveX = Input.GetAxis(str_horiz) * Time.deltaTime;
        moveY = Input.GetAxis(str_vertic) * Time.deltaTime;
        transform.Translate(moveX, moveY, 0);

        if (moveX == 0 && moveY == 0) {
            anim.SetInteger(str_dir, 0);
            Debug.Log("Veio aqui!");
        } else if (Mathf.Abs(moveX) > Mathf.Abs(moveY)) {
            if (moveX > 0 && direction != 2)
                anim.SetInteger(str_dir, 2);
            else
                anim.SetInteger(str_dir, 4);
        } else {
            if (moveY > 0 && direction != 1)
                anim.SetInteger(str_dir, 1);
            else
                anim.SetInteger(str_dir, 3);
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag.Equals("LockedAred")) {
            transform.position = anteriorPosOnWalkingArea;
        }
    }

    private string str_WalkArea = "WalkArea";
    private string str_areaOfLevel = "AreaOfLevel";
    void OnTriggerStay2D(Collider2D collision) {
        if (selectedLevel == -1) {
            for (int i = 0; i < levelsNames.Length; i++) {
                if (collision.gameObject.name.Equals(levelsNames[i])) {
                    selectedLevel = i;
                    collision.GetComponent<LevelOnMap>().playerOver();
                }
            }
        }

        if (collision.gameObject.tag.Equals(str_WalkArea) && Vector3.Distance(anteriorPosOnWalkingArea, transform.position) > 1f) {
            anteriorPosOnWalkingArea = transform.position;
        }
        
        if (collision.gameObject.tag.Equals(str_areaOfLevel)) {
            
            if (onArea == null || !collision.gameObject.Equals(onArea)) {
                onArea = collision.gameObject;
                Debug.Log("Area " + onArea.name);
            }
        }

    }
    
    void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag.Equals("LevelSelect")) {
            selectedLevel = -1;
            collision.GetComponent<LevelOnMap>().playerExit();
        } else if (collision.gameObject.tag.Equals("WalkArea")) {
            transform.position = anteriorPosOnWalkingArea;
        }
    }

    private void loadGameLevel() {
        if (selectedLevel >= 0) {
            if (PlayerData.instance._levelsSituation[selectedLevel] == 0)
                UnityEngine.SceneManagement.SceneManager.LoadScene(levelsNames[selectedLevel]);
            else
                UnityEngine.SceneManagement.SceneManager.LoadScene("DeliverySouls");
        }
    }

}

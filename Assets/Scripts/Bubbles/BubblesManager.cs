using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubblesManager : MonoBehaviour {

    public static BubblesManager instance;

    public BubbleController bubble;
    public int quant;

    private BubbleController selectedBubble;
    public BubbleController _selectedBubble { get { return selectedBubble; } }
    
    private List<BubbleController> availableBubbles;
    private List<BubbleController> bubblesOnGame;

    private int currentSortingOrder = 50;

    private void Awake() {
        instance = this;
    }

    // Use this for initialization
    void Start () {
        availableBubbles = new List<BubbleController>();
        bubblesOnGame = new List<BubbleController>();
        for (int i = 0; i < quant; i++) {
            availableBubbles.Add(Instantiate(bubble, transform.position, Quaternion.identity));
            availableBubbles[i].transform.Translate(-10,0,0);
            availableBubbles[i].transform.parent = transform;
            availableBubbles[i].gameObject.SetActive(false);
            availableBubbles[i].GetComponent<SpriteRenderer>().sortingOrder = currentSortingOrder;
            availableBubbles[i].transform.Find("b_bright").GetComponent<SpriteRenderer>().sortingOrder = currentSortingOrder + 1;
            availableBubbles[i].transform.Find("b_ring").GetComponent<SpriteRenderer>().sortingOrder = currentSortingOrder + 2;
            availableBubbles[i].transform.Find("b_ring").gameObject.SetActive(false);
            currentSortingOrder += 5;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            useHotKeyBubble(selectOneBubble(EnumsGame.BubbleCollors.RED));
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            useHotKeyBubble(selectOneBubble(EnumsGame.BubbleCollors.YELLOW));
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            useHotKeyBubble(selectOneBubble(EnumsGame.BubbleCollors.BLUE));
        
    }

    private BubbleController selectOneBubble(EnumsGame.BubbleCollors cor) {
        foreach(BubbleController b in bubblesOnGame) {
            if (!b._used && b.collor.Equals(cor)) {
                return b;
            }
        }
        return null;
    }

    private void useHotKeyBubble(BubbleController b) {

        if(b != null)
            b.atackEnemy(EnemiesManager.instance._enemyOnTarget);
    }

    public void setSelectedBubble(BubbleController selected) {
        selectedBubble = selected;
    }

    public BubbleController getABubble() {
        if(availableBubbles.Count > 0) {
            bubblesOnGame.Add(availableBubbles[0]);
            availableBubbles.Remove(availableBubbles[0]);
            bubblesOnGame[bubblesOnGame.Count - 1].gameObject.SetActive(true);
            return bubblesOnGame[bubblesOnGame.Count - 1];
        }
        return null;
    }

    public void removeBubbleFromScene(BubbleController bu) {
        bubblesOnGame.Remove(bu);
        availableBubbles.Add(bu);
        bu.gameObject.SetActive(false);
    }
    
    public int quantBubblesOnGame() {
        return bubblesOnGame.Count;
    }

    public int quantBubblesNotUsed() {
        
        int retorno = 0;
        foreach (BubbleController bc in bubblesOnGame)
            if (!bc._used)
                retorno++;
        return retorno;
        
    }

    public int quantBubblesType(EnumsGame.BubbleCollors collor) {

        int retorno = 0;
        foreach (BubbleController bc in bubblesOnGame)
            if (bc.collor.Equals(collor))
                retorno++;
        return retorno;

    }

    public void explodesAllBubbles() {
        for (int i = bubblesOnGame.Count-1; i >= 0; i--) {
            removeBubbleFromScene(bubblesOnGame[i]);
        }
    }

    public void activeBubbles() {
        foreach (BubbleController bc in bubblesOnGame)
            bc.allowsTheUse();
    }

    //Is a role of PlayerCreateBubble
    /*
    public int quantBubblesNotUsed() {
        int quant = 0;

        foreach (BubbleController bc in bubblesOnGame)
            if (!bc._used)
                quant++;

        Debug.Log(quant);

        return quant;
    }
    */

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour {

    public static EnemiesManager instance;

    public GameObject endGameCave;

    private EnemyController enemyOnTarget;
    public EnemyController _enemyOnTarget { get { return enemyOnTarget; } set { enemyOnTarget = value; } }

    
    private List<EnemyController> enemies;

    void Awake() {
        instance = this;
    }

    // Use this for initialization
    void Start() {
        enemies = new List<EnemyController>();
        initNextEnemies();
        if (!GameplayManager.instance.gameType.Equals(EnumsGame.GameplayType.LEVEL_GAME)) {
            for(int i = 0; i < transform.childCount; i++) {
                enemies.Add(transform.GetChild(i).GetComponent<EnemyController>());
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (enemies.Count == 0) {
            if(GameplayManager.instance.gameType.Equals(EnumsGame.GameplayType.FIGHT_GAME))
                GameplayManager.instance.openWinPanel();
            else if (GameplayManager.instance.gameType.Equals(EnumsGame.GameplayType.LEVEL_GAME)) {
                GameplayManager.instance.advanceOnLevel();
            }
        }
    }

    public void initNextEnemies() {

        if (!GameplayManager.instance.gameType.Equals(EnumsGame.GameplayType.LEVEL_GAME))
            return;
        
        if (transform.childCount > 0) {

            if (transform.childCount == 1) {
                endGameCave.SetActive(true);
                Debug.Log("GAMB: Parte que ativa a imagem da caverna.");
            }

            Transform t = transform.GetChild(0);
            t.gameObject.SetActive(true);
            for (int i = t.childCount-1; i >= 0; i--) {
                enemies.Add(t.GetChild(i).GetComponent<EnemyController>());
                t.GetChild(i).parent = this.transform;
            }
            Destroy(t.gameObject);
            
        } else {
            GameplayManager.instance.openWinPanel();
            Debug.Log("Abrir Painel Final quando não tiver mais inimigos!");
        }
    }

    public void initFlashEarthQuake() {

        //The hit od EarthQuake is based on value of fireBase and the EARTHQUAKE level.
        foreach (EnemyController en in enemies)
            en.hitMe((PlayerData.instance._fireBase + PlayerData.instance.getSkillLevel(EnumsGame.GameSkills.EARTHQUAKE)) * 3);
    }

    public void initFlashPoison() {
        //The hit od EarthQuake is based on value of fireBase and the EARTHQUAKE level.
        foreach (EnemyController en in enemies)
            en.GetComponent<EnemyBuffDebuff>().addPoisonDebuff();
    }

    public void initFlashArkane() {
        Debug.Log("Falta melhorar o Arkane para quando o level da magia aumentar");
        foreach (EnemyController en in enemies) {
            en.addWaterBasicEffect();
        }
    }

    public void removeEnemyFromScene(EnemyController enemy) {
        Debug.Log("Implementação temporária para remover monstro. Não está implementado o POOL ainda.");
        enemies.Remove(enemy);
        Destroy(enemy.gameObject);
    }

    /*
    private void flashPoison() {
        
        int hit = PlayerData.instance._eletricBase;
        float timeToHit = 2;

        if (PlayerData.instance.getSkillLevel(EnumsGame.GameSkills.POISON) == 2)
            timeToHit = 1.8f;
        else if (PlayerData.instance.getSkillLevel(EnumsGame.GameSkills.POISON) == 3)
            timeToHit = 1.6f;
        else if (PlayerData.instance.getSkillLevel(EnumsGame.GameSkills.POISON) == 4) {
            timeToHit = 1.6f;
            hit = 2;
        } else if (PlayerData.instance.getSkillLevel(EnumsGame.GameSkills.POISON) == 5) {
            timeToHit = 1.5f;
            hit = 2;
        } else if (PlayerData.instance.getSkillLevel(EnumsGame.GameSkills.POISON) == 6) {
            timeToHit = 1.4f;
            hit = 2;
        } else if (PlayerData.instance.getSkillLevel(EnumsGame.GameSkills.POISON) == 7) {
            timeToHit = 1.4f;
            hit = 3;
        } else if (PlayerData.instance.getSkillLevel(EnumsGame.GameSkills.POISON) == 8) {
            timeToHit = 1.3f;
            hit = 3;
        } else if (PlayerData.instance.getSkillLevel(EnumsGame.GameSkills.POISON) == 9) {
            timeToHit = 1.2f;
            hit = 3;
        } else if (PlayerData.instance.getSkillLevel(EnumsGame.GameSkills.POISON) == 10) {
            timeToHit = 1.2f;
            hit = 4;
        }
    
    }
    */

}

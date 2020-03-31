using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapManager : MonoBehaviour {

    private Transform followTarget;
    private float cameraDistance;

    // Use this for initialization
    void Start() {
        followTarget = PlayerOnWorldMap.instance.transform;
        cameraDistance = PlayerOnWorldMap.instance.transform.position.z - transform.position.z;
        Invoke("loadBattleScene", Random.Range(10.0f, 40.0f));

        new PlayerData();

    }

    // Update is called once per frame
    void Update() {
        if (!transform.position.Equals(followTarget.transform.position))
            transform.position = Vector3.Lerp(transform.position, followTarget.position + Vector3.back * cameraDistance, Time.deltaTime * 1f);
    }

    void loadBattleScene() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(AreaLevelManager.instance.getOneSceneOfLevel(int.Parse(
                                                                                PlayerOnWorldMap.instance._onArea.name)));
    }

    public void addPlayerExp(int quant) {
        PlayerData.instance.addExp(quant);
    }

}
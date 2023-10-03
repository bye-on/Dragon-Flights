using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManager : MonoBehaviour
{
    List<Spawn> spawnList;
    public int spawnIndex;
    public bool spawnEnd;

    public Text scoreText;
    public Image[] lifeImage;
    public GameObject gameOverSet;

    public GameObject[] enemyObjs;
    public Transform[] spawnPoints;
    public GameObject player;
    public float nextSpawnDelay;
    public float curSpawnDelay;

    void Awake() {
        spawnList = new List<Spawn>();
        ReadSpawnFile();
    }

    void ReadSpawnFile() {
        spawnList.Clear();
        spawnIndex = 0;
        spawnEnd = false;

        TextAsset textAsset = Resources.Load("Stage 0") as TextAsset;
        StringReader stringReader = new StringReader(textAsset.text);

        while(stringReader != null) {    
            string line = stringReader.ReadLine();
            Debug.Log(line);

            if(line == null) break;

            Spawn spawnData = new Spawn();
            spawnData.delay = float.Parse(line.Split(',')[0]);
            spawnData.type = line.Split(',')[1];
            spawnData.point = int.Parse(line.Split(',')[2]);
            spawnList.Add(spawnData);
        }
        stringReader.Close();

        nextSpawnDelay = spawnList[0].delay;
    }
    
    void SpawnEnemy() {
        int enemyIndex = 0;
        switch(spawnList[spawnIndex].type) {
            case "S":
                enemyIndex = 0;
                break;
            case "M":
                enemyIndex = 1;
                break;
            case "L":
                enemyIndex = 2;
                break;
        }

        int pointIndex = spawnList[spawnIndex].point;

        // int randomEnemy = Random.Range(0, 3);
        // int randomPoint = Random.Range(0, 5);
        GameObject enemy = Instantiate(enemyObjs[enemyIndex], 
            spawnPoints[pointIndex].position, spawnPoints[pointIndex].rotation);
        RandomEnemy enemyLogic = enemy.GetComponent<RandomEnemy>();
        enemyLogic.player = player;

        spawnIndex++;
        if(spawnIndex == spawnList.Count) {
            spawnEnd = true;
            return;
        }

        nextSpawnDelay = spawnList[spawnIndex].delay;
    }

    // Update is called once per frame
    void Update()
    {
        curSpawnDelay += Time.deltaTime;

        if(curSpawnDelay > nextSpawnDelay && !spawnEnd) {
            SpawnEnemy();
            // nextSpawnDelay = Random.Range(0.5f, 3f);
            curSpawnDelay = 0;
        }

        Player playerLogic = player.GetComponent<Player>();
        scoreText.text = string.Format("{0:n0}", playerLogic.score);
    }
    public void UpdateLife(int life) {
        for(int i = 0; i < lifeImage.Length; i++) { //init
            lifeImage[i].color = new Color(1, 1, 1, 0);
        }
        for(int i = 0; i < life; i++) {
            lifeImage[i].color = new Color(1, 1, 1, 1);
        }
    }
    public void GameOver() {
        gameOverSet.SetActive(true);
    }
    public void GameRetry() {
        SceneManager.LoadScene(0);
    }
    public void RespawnManager() {
        Invoke("Respawn", 2);
    }
    void Respawn() {
        player.transform.position = Vector2.down * 3.5f;
        player.SetActive(true);

        Player playerLogic = player.GetComponent<Player>();
        playerLogic.isHit = false;
    }
}

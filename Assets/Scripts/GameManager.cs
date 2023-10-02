using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Text scoreText;
    public Image[] lifeImage;
    public GameObject gameOverSet;

    public GameObject[] enemyObjs;
    public Transform[] spawnPoints;
    public GameObject player;
    public float maxSpawnDelay;
    public float curSpawnDelay;
    
    void SpawnEnemy() {
        int randomEnemy = Random.Range(0, 3);
        int randomPoint = Random.Range(0, 5);
        GameObject enemy = Instantiate(enemyObjs[randomEnemy], 
            spawnPoints[randomPoint].position, spawnPoints[randomPoint].rotation);
        RandomEnemy enemyLogic = enemy.GetComponent<RandomEnemy>();
        enemyLogic.player = player;
    }

    // Update is called once per frame
    void Update()
    {
        curSpawnDelay += Time.deltaTime;

        if(curSpawnDelay > maxSpawnDelay) {
            SpawnEnemy();
            maxSpawnDelay = Random.Range(0.5f, 3f);
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

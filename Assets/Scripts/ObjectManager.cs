using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public GameObject enemyLPrefab;
    public GameObject enemyMPrefab;
    public GameObject enemySPrefab;

    public GameObject bulletPlayerPrefab;
    public GameObject bulletEnemyPrefab;
    public GameObject bulletBossAPrefab;
    public GameObject bulletBossBPrefab;

    GameObject[] enemyL;
    GameObject[] enemyM;
    GameObject[] enemyS;

    GameObject[] bulletPlayer;
    GameObject[] bulletEnemy;
    GameObject[] bulletBossA;
    GameObject[] bulletBossB;

    GameObject[] targetPool;

    void Awake() {
        enemyL = new GameObject[10];
        enemyM = new GameObject[10];
        enemyS = new GameObject[20];

        bulletPlayer = new GameObject[50];
        bulletEnemy = new GameObject[100];
        bulletBossA = new GameObject[100];
        bulletBossB = new GameObject[200];

        Generate();
    }

    void Generate() {
        for(int i = 0; i < enemyL.Length; i++) {
            enemyL[i] = Instantiate(enemyLPrefab);
            enemyL[i].SetActive(false);
        }
        for(int i = 0; i < enemyM.Length; i++) {
            enemyM[i] = Instantiate(enemyMPrefab);
            enemyM[i].SetActive(false);
        }
        for(int i = 0; i < enemyS.Length; i++) {
            enemyS[i] = Instantiate(enemySPrefab);
            enemyS[i].SetActive(false);
        }

        for(int i = 0; i < bulletPlayer.Length; i++) {
            bulletPlayer[i] = Instantiate(bulletPlayerPrefab);
            bulletPlayer[i].SetActive(false);
        }
        for(int i = 0; i < bulletEnemy.Length; i++) {
            bulletEnemy[i] = Instantiate(bulletEnemyPrefab);
            bulletEnemy[i].SetActive(false);
        }
        for(int i = 0; i < bulletBossA.Length; i++) {
            bulletBossA[i] = Instantiate(bulletBossAPrefab);
            bulletBossA[i].SetActive(false);
        }
        for(int i = 0; i < bulletBossB.Length; i++) {
            bulletBossB[i] = Instantiate(bulletBossBPrefab);
            bulletBossB[i].SetActive(false);
        }
    }

    public GameObject MakeObject(string name) { // 오브젝트 풀에 접근할 수 있는 함수
        switch (name) {
            case "EnemyL":
                targetPool = enemyL;
                break;
            case "EnemyM":
                targetPool = enemyM;
                break;
            case "EnemyS":
                targetPool = enemyS;
                break;
            
            case "BulletPlayer":
                targetPool = bulletPlayer;
                break;
            case "BulletEnemy":
                targetPool = bulletEnemy;
                break;
            case "BulletBossA":
                targetPool = bulletBossA;
                break;
            case "BulletBossB":
                targetPool = bulletBossB;
                break;
        }

        for (int i = 0; i < targetPool.Length; i++) {
            if(!targetPool[i].activeSelf) {
                targetPool[i].SetActive(true);
                return targetPool[i];
            }
        }
        return null;
    }
}

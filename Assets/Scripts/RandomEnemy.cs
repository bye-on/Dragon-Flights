using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnemy : MonoBehaviour
{
    public float speed;
    public int hp;
    public int enemyScore;

    public GameObject bulletPrefab;
    public GameObject bossBulletPrefabA;
    public GameObject bossBulletPrefabB;
    public string enemyName;

    public float maxBulletDelay;
    public float curBulletDelay;

    public GameObject player;
    public ObjectManager objectManager;

    Rigidbody2D rigid;
    SpriteRenderer sprite;

    public int patternIndex;
    public int currentPatternCnt;
    public int[] maxPatternCnt;

    void Awake() {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        rigid.velocity = Vector2.down * speed;    
    }
    void Update() {
        if(enemyName == "B") 
            return;
        Fire();
        curBulletDelay += Time.deltaTime;
    }

    void OnHit(int damage) {
        hp -= damage;
        sprite.color = new Color(0.8f, 0.8f, 0.8f, 1);
        Invoke("ReturnSprite", 0.05f);
        if(hp <= 0) {
            Player playerLogic = player.GetComponent<Player>();
            playerLogic.score += enemyScore;
            gameObject.SetActive(false);
        }
    }
    void ReturnSprite() {
        sprite.color = new Color(1f, 1f, 1f, 1);
    }
    void OnEnable() {
        switch(enemyName) {
            case "B":
                hp = 3000;
                Invoke("Stop", 2);
                break;
            case "L":
                hp = 40;
                break;
            case "M":
                hp = 10;
                break;
            case "S":
                hp = 3;
                break;
        }
    }
    void Stop() {
        if(!gameObject.activeSelf) return;

        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.zero;

        Invoke("Pattern", 2);
    }
    void Pattern() {
        patternIndex = patternIndex == 3 ? 0 : patternIndex + 1;
        currentPatternCnt = 0;

        switch(patternIndex) {
            case 0:
                FireForward();
                break;
            case 1:
                FireShot();
                break;
            case 2:
                FireArc();
                break;
            case 3:
                FireAround();
                break;
        }
    }

    void FireForward() { // 앞으로 4발 발사
        GameObject bulletL = objectManager.MakeObject("BulletBossA");
        bulletL.transform.position = transform.position + Vector3.left * 0.1f + Vector3.down * 2f;
        GameObject bulletR = objectManager.MakeObject("BulletBossA");
        bulletL.transform.position = transform.position + Vector3.right * 0.1f + Vector3.down * 2f;
        GameObject bulletLL = objectManager.MakeObject("BulletBossA");
        bulletL.transform.position = transform.position + Vector3.left * 0.5f + Vector3.down * 2;
        GameObject bulletRR = objectManager.MakeObject("BulletBossA");
        bulletL.transform.position = transform.position + Vector3.right * 0.5f + Vector3.down * 2f;
        

        Rigidbody2D rigidBulletL = bulletL.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidBulletR = bulletR.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidBulletLL = bulletLL.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidBulletRR = bulletRR.GetComponent<Rigidbody2D>();

        rigidBulletL.AddForce(Vector2.down * 12, ForceMode2D.Impulse);
        rigidBulletR.AddForce(Vector2.down * 12, ForceMode2D.Impulse);
        rigidBulletLL.AddForce(Vector2.down * 12, ForceMode2D.Impulse);
        rigidBulletRR.AddForce(Vector2.down * 12, ForceMode2D.Impulse);

        currentPatternCnt++;

        if(currentPatternCnt < maxPatternCnt[patternIndex])
            Invoke("FireForward", 2);
        else
            Invoke("Pattern", 2);
    }
    void FireShot() { // 플레이어 방향으로 샷건
        for(int i = 0; i < 5; i++) {
            GameObject bullet = objectManager.MakeObject("BulletBossA");
            bullet.transform.position = transform.position + Vector3.down * 2;
            Rigidbody2D rigidBullet = bullet.GetComponent<Rigidbody2D>();
            Vector2 dirVec = player.transform.position - transform.position;
            Vector2 ranVec = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(0f, 2f));
            dirVec += ranVec;

            rigidBullet.AddForce(dirVec.normalized * 10, ForceMode2D.Impulse);
        }

        currentPatternCnt++;

        if(currentPatternCnt < maxPatternCnt[patternIndex])
            Invoke("FireShot", 3.5f);
        else
            Invoke("Pattern", 2);
    }
    void FireArc() { // 부채 모양으로
        GameObject bullet = objectManager.MakeObject("BulletBossA");
        bullet.transform.position = transform.position;
        bullet.transform.rotation = Quaternion.identity;

        Rigidbody2D rigidBullet = bullet.GetComponent<Rigidbody2D>();
        Vector2 dirVec = new Vector2(Mathf.Sin(Mathf.PI * 4 * currentPatternCnt/maxPatternCnt[patternIndex]), -1);

        rigidBullet.AddForce(dirVec.normalized * 8, ForceMode2D.Impulse);
        
        currentPatternCnt++;

        if(currentPatternCnt < maxPatternCnt[patternIndex])
            Invoke("FireArc", 0.15f);
        else
            Invoke("Pattern", 2);
    }

    void FireAround() { // 원 형태로 전체 공격
        int roundNumA = 50;
        int roundNumB = 40;
        int roundNum = currentPatternCnt % 2 == 0 ? roundNumA : roundNumB;

        for(int i = 0; i < roundNum; i++) {
            GameObject bullet = objectManager.MakeObject("BulletBossB");
            bullet.transform.position = transform.position;
            bullet.transform.rotation = Quaternion.identity;

            Rigidbody2D rigidBullet = bullet.GetComponent<Rigidbody2D>();
            Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 2f * i / roundNum), 
                                        Mathf.Sin(Mathf.PI * 2f * i / roundNum));

            rigidBullet.AddForce(dirVec.normalized * 2, ForceMode2D.Impulse);

            Vector3 rotVec = Vector3.forward * 360 * i / roundNum + Vector3.forward * 90;
            bullet.transform.Rotate(rotVec);
        }
        
        currentPatternCnt++;

        if(currentPatternCnt < maxPatternCnt[patternIndex])
            Invoke("FireAround", 0.7f);
        else
            Invoke("Pattern", 2);
    }
    
     void Fire() {
        if(curBulletDelay < maxBulletDelay) return;

        if(enemyName == "L") {
            GameObject bullet = objectManager.MakeObject("BulletEnemy");
            bullet.transform.position = transform.position + Vector3.down;
            bullet.GetComponent<SpriteRenderer>().flipY = true;
            Rigidbody2D rigidBullet = bullet.GetComponent<Rigidbody2D>();
            Vector3 dirVec = player.transform.position - transform.position;

            rigidBullet.AddForce(dirVec.normalized * 7, ForceMode2D.Impulse);
        }
        else if (enemyName == "M") {
            GameObject bulletL = objectManager.MakeObject("BulletEnemy");
            bulletL.transform.position = transform.position + Vector3.left * 0.2f + Vector3.down;
            GameObject bulletR = objectManager.MakeObject("BulletEnemy");
            bulletR.transform.position = transform.position + Vector3.right * 0.2f + Vector3.down;
            bulletL.GetComponent<SpriteRenderer>().flipY = true;
            bulletR.GetComponent<SpriteRenderer>().flipY = true;
            Rigidbody2D rigidBulletL = bulletL.GetComponent<Rigidbody2D>();
            Rigidbody2D rigidBulletR = bulletR.GetComponent<Rigidbody2D>();
            rigidBulletL.AddForce(Vector2.down * 7, ForceMode2D.Impulse);
            rigidBulletR.AddForce(Vector2.down * 7, ForceMode2D.Impulse);
        }
    
        curBulletDelay = 0;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "BorderBullet") {
            gameObject.SetActive(false);
            transform.rotation = Quaternion.identity;
        }
        else if(other.gameObject.tag == "PlayerBullet") {
            Bullet bullet = other.gameObject.GetComponent<Bullet>();
            OnHit(bullet.damage);
            other.gameObject.SetActive(false);
        }
    }
}

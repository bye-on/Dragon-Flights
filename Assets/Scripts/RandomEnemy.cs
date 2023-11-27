using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnemy : MonoBehaviour
{
    public float speed;
    public int hp;
    public int enemyScore;

    public GameObject bulletPrefab;
    public string enemyName;

    public float maxBulletDelay;
    public float curBulletDelay;

    public GameObject player;

    Rigidbody2D rigid;
    SpriteRenderer sprite;

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
            Destroy(gameObject);
        }
    }
    void ReturnSprite() {
        sprite.color = new Color(1, 1, 1, 1);
    }
     void Fire() {
        if(curBulletDelay < maxBulletDelay) return;

        if(enemyName == "L") {
            GameObject bullet = Instantiate(bulletPrefab, transform.position + Vector3.down, transform.rotation);
            bullet.GetComponent<SpriteRenderer>().flipY = true;
            Rigidbody2D rigidBullet = bullet.GetComponent<Rigidbody2D>();
            Vector3 dirVec = player.transform.position - transform.position;

            rigidBullet.AddForce(dirVec.normalized * 10, ForceMode2D.Impulse);
        }
        else if (enemyName == "M") {
            GameObject bulletL = Instantiate(bulletPrefab, transform.position + Vector3.left * 0.2f + Vector3.down, transform.rotation);
            GameObject bulletR = Instantiate(bulletPrefab, transform.position + Vector3.right * 0.2f + Vector3.down, transform.rotation);
            bulletL.GetComponent<SpriteRenderer>().flipY = true;
            bulletR.GetComponent<SpriteRenderer>().flipY = true;
            Rigidbody2D rigidBulletL = bulletL.GetComponent<Rigidbody2D>();
            Rigidbody2D rigidBulletR = bulletR.GetComponent<Rigidbody2D>();
            rigidBulletL.AddForce(Vector2.down * 10, ForceMode2D.Impulse);
            rigidBulletR.AddForce(Vector2.down * 10, ForceMode2D.Impulse);
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

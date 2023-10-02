using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnemy : MonoBehaviour
{
    public float speed;
    public int hp;

    Rigidbody2D rigid;

    void Awake() {
        rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.down * speed;    
    }

    void OnHit(int damage) {
        hp -= damage;
        if(hp <= 0) {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "BorderBullet") {
            Destroy(gameObject);
        }
        else if(other.gameObject.tag == "PlayerBullet") {
            Bullet bullet = other.gameObject.GetComponent<Bullet>();
            OnHit(bullet.damage);
            Destroy(other.gameObject);
        }
    }
}

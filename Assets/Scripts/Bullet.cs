using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "BorderBullet") {
            gameObject.SetActive(false);
        }
    }
}

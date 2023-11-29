using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public int life;
    public int score;
    public bool isHit;
    
    public bool isTouchTop;
    public bool isTouchBottom;
    public bool isTouchLeft;
    public bool isTouchRight;

    public GameObject bulletPrefab;
    public float maxShotDelay;
    public float curShotDelay;

    public GameManager gameManager;
    public ObjectManager objectManager;

    void Update() {
        if(Input.GetMouseButton(0)) {
            OnMouseDrag();    
        }
        Fire();
        curShotDelay += Time.deltaTime;
    }
    void Fire() {
        if(curShotDelay < maxShotDelay)
            return;
        GameObject bullet = objectManager.MakeObject("BulletPlayer");
        bullet.transform.position = transform.position;

        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

        curShotDelay = 0;
    }
    void OnMouseDrag() {
        float h = Input.mousePosition.x;

        // 세로 이동 안됨
        // float v = Input.mousePosition.y;
        
        // if(isTouchTop & v >= Screen.height) v = Screen.height;
        // if(isTouchBottom && v <= 0) v = 0;
        if(isTouchLeft && h <= 0) h = 0;
        if(isTouchRight && h >= Screen.width) h = Screen.width;
        
        Vector2 mousePos = new Vector2(h, 0);
        Vector2 objPos = Camera.main.ScreenToWorldPoint(mousePos);
        transform.position = new Vector2(objPos.x, -3.5f);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Border") {
            switch (other.gameObject.name) {
                case "Top":
                    isTouchTop = true;
                    break;
                case "Bottom":
                    isTouchBottom = true;
                    break;
                case "Left":
                    isTouchLeft = true;
                    break;
                case "Right":
                    isTouchRight = true;
                    break;
            }
        }
        else if((other.gameObject.tag == "Enemy") || (other.gameObject.tag == "EnemyBullet")) {
            if(isHit) return;

            life--;
            gameManager.UpdateLife(life);
            isHit = true;

            if(life == 0) gameManager.GameOver();
            else gameManager.RespawnManager();

            gameObject.SetActive(false);
            other.gameObject.SetActive(false);
            // Destroy(other.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Border") {
            switch (other.gameObject.name) {
                case "Top":
                    isTouchTop = false;
                    break;
                case "Bottom":
                    isTouchBottom = false;
                    break;
                case "Left":
                    isTouchLeft = false;
                    break;
                case "Right":
                    isTouchRight = false;
                    break;
            }
        }
    }
}

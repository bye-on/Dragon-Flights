using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEasyAttack : MonoBehaviour
{
    public GameObject bulletPrefab;

    public float maxBulletDelay;
    public float curBulletDelay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
        curBulletDelay += Time.deltaTime;
    }
    void Fire() {
        if(curBulletDelay < maxBulletDelay) return;
        
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

        curBulletDelay = 0;
    }
}

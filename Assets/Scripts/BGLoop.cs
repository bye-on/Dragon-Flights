using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGLoop : MonoBehaviour
{
    private BoxCollider2D background;
    private float height;
    public float speed = 10f;
    void Awake() {
        background = GetComponent<BoxCollider2D>();
        height = 2 * background.size.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(0, Time.deltaTime * speed));
        if(transform.position.y >= height) {
            Reposiion();
        }
    }
    private void Reposiion() {
        Vector2 offset = new Vector2(0, 2 * height);
        // transform.position = (Vector2)transform.position - offset;
        transform.Translate(-offset);
    }
}

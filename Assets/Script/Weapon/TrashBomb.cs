using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBomb : MonoBehaviour {

    public float BombSpeed = 200.0f;
    public Vector2 Position;

    public Rigidbody2D rb;
    public BoxCollider2D box2D;

    void Start() {
        Position = new Vector2(transform.position.x - 2.5f, transform.position.y - 2.5f);
        box2D.enabled = false;
    }

    void Update() {
        followPosition();
    }

    public void followPosition() {
        Vector2 direction = Position - (Vector2)this.transform.position;
        float distance = direction.magnitude;

        if(distance > 0.1f) {
            box2D.enabled = true;
            Vector2 movement = direction.normalized * BombSpeed * Time.deltaTime;
            rb.MovePosition(rb.position + movement);
        }
        else {
            Destroy(gameObject, 0.2f);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        string layerName = LayerMask.LayerToName(other.gameObject.layer);
        
        if (layerName == "Enemy") {
            Destroy(gameObject, 0.2f);
        }
        else {
            Destroy(gameObject, 0.2f);
        }
    }
}
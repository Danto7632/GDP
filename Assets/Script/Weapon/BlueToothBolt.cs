using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueToothBolt : MonoBehaviour {

    public float BoltSpeed = 0.5f;

    public Rigidbody2D rb;
    public BoxCollider2D box2D;

    void Update() {
        followEnemy();
    }

    void followEnemy() {
        transform.Translate(Vector2.down * BoltSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other) {
        string layerName = LayerMask.LayerToName(other.gameObject.layer);
        
        if (layerName == "Enemy" && this.gameObject.transform.parent == other.gameObject.transform) {
            box2D.enabled = false;
            rb.constraints |= RigidbodyConstraints2D.FreezePositionY;
            Destroy(gameObject, 0.2f);
        }
        else {
            Destroy(gameObject, 0.2f);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacketBullet : MonoBehaviour {
    public Transform enemyPosition;
    public float packetSpeed = 20.0f;

    public Rigidbody2D rb;

    void Update() {
        if (!rb || rb.IsSleeping()) {
            // Rigidbody가 없거나 이미 소멸된 경우
            return;
        }

        followEnemy();
    }

    public void FindEnemy(Collider2D Enemy) {
        if (Enemy != null && Enemy.transform != null) {
            enemyPosition = Enemy.transform;
        }
    }

    void followEnemy() {
        if (enemyPosition != null) {
            Vector2 direction = (Vector2)enemyPosition.position - rb.position;

            Vector2 movement = direction.normalized * packetSpeed * Time.deltaTime;
            rb.MovePosition(rb.position + movement);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other == null) {
            return; // null 체크
        }

        string layerName = LayerMask.LayerToName(other.gameObject.layer);
        
        if (layerName == "Enemy") {
            Destroy(gameObject);
        } else {
            Destroy(gameObject, 3.0f);
        }
    }
}

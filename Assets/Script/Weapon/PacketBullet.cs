using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacketBullet : MonoBehaviour {
    public Transform enemyPosition;
    public GameObject Enemy;
    public float packetSpeed = 20.0f;

    public Rigidbody2D rb;

    void Start() {
        FindEnemy();
    }

    void Update() {
        followEnemy();
    }

    void FindEnemy() {
        Enemy = GameObject.FindWithTag("Enemy");
        enemyPosition = Enemy.transform;
    }

    void followEnemy() {
        Vector2 direction = enemyPosition.position - this.transform.position;
        
        Vector2 movement = direction.normalized * packetSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + movement);
    }

    void OnTriggerEnter2D(Collider2D other) {
        switch(LayerMask.LayerToName(other.gameObject.layer)) {    
            case "Enemy" :
                Destroy(gameObject);
                break;
        }
    }
}

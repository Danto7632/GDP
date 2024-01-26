using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacketBullet : MonoBehaviour {
    private List<GameObject> enemies;
    private GameObject closestEnemy;

    public float packetSpeed = 30.0f;
    public float closestDistance = float.PositiveInfinity;
    public Vector2 currentPosition;
    public Vector2 enemyPosition;

    public Rigidbody2D rb;

    void Start() {
        enemies = new List<GameObject>();
        FindEnemy();
    }

    void Update() {
        if (!rb || rb.IsSleeping()) {
            // Rigidbody가 없거나 이미 소멸된 경우
            return;
        }
        followEnemy(closestEnemy);
    }

    public void FindEnemy() {
        enemies.Clear();
        currentPosition = transform.position;

        GameObject[] enemyArray = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in enemyArray) {
            enemies.Add(enemy);
            enemyPosition = enemy.transform.position;

            if(Vector2.Distance(currentPosition, enemyPosition) < closestDistance) {
                closestDistance = Vector2.Distance(currentPosition, enemyPosition);
                closestEnemy = enemy;
            }
        }
    }

    void followEnemy(GameObject enemy) {
        if (enemy != null) {
            Vector2 direction = (Vector2)enemy.transform.position - rb.position;

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
        }
        else {
            Destroy(gameObject, 3.0f);
        }
    }
}

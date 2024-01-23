using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour {
    public GameObject Player;
    public Transform playerPosition;
    public Transform Enemy;

    public Rigidbody2D rb;
    public SpriteRenderer sp;

    public float speed = 2.0f;

    void Start() {
        FindPlayer();
    }

    void Update() {
        followPlayer();
    }

    void FindPlayer() {
        Player = GameObject.FindWithTag("Player");
        playerPosition = Player.transform;
    }

    void followPlayer() {
        Vector2 direction = playerPosition.position - this.transform.position;
        float distance = direction.magnitude;

        if(distance > 0.1f) {
            Vector2 movement = direction.normalized * speed * Time.deltaTime;
            rb.MovePosition(rb.position + movement);
        }
        else {
            Debug.Log("idk");
            FindPlayer();
        }
    }
}
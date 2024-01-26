using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour {
    public GameObject Player;
    public Transform playerPosition;
    public Transform Enemy;

    public Rigidbody2D rb;
    public SpriteRenderer sp;

    public float speed = 15.0f;
    public int Hp = 2;

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

            // 수정된 부분
            Flip(movement);
        }
        else {
            FindPlayer();
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        switch(LayerMask.LayerToName(other.gameObject.layer)) {    
            case "Packet" :
                Debug.Log("Hit");
                Hp--;
                if(Hp <= 0) {
                    Destroy(gameObject);
                }
                break;
        }
    }

    // 수정된 플립 함수
    void Flip(Vector2 movement) {
        if (movement.x > 0) {
            sp.flipX = false; // 오른쪽으로 갈 때는 flipX를 false로 설정
        } else if (movement.x < 0) {
            sp.flipX = true; // 왼쪽으로 갈 때는 flipX를 true로 설정
        }
    }
}

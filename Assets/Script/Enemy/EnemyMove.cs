using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour {
    public GameObject Player;
    public GameObject ExpPrefab;
    public GameObject Exp;
    public GameObject ExpNode;

    public Transform playerPosition;
    public Transform Enemy;

    public Rigidbody2D rb;
    public SpriteRenderer sp;

    public SpriteRenderer wifiSp;

    public float speed = 15.0f;
    public int Hp = 2;
    public int ExpValue = 1;

    public float plusVectorX;
    public float plusVectorY;

    public bool isWifi = false;

    void Start() {
        FindPlayer();
        ExpNode = GameObject.FindWithTag("ExpNode");
    }

    void Update() {
        followPlayer();
    }

    void FindPlayer() {
        Player = GameObject.FindWithTag("Player");
        playerPosition = Player.transform;
    }

    void followPlayer() {
       if(!isWifi) {
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
    }

    void OnTriggerEnter2D(Collider2D other) {
        switch(LayerMask.LayerToName(other.gameObject.layer)) {    
            case "Packet" :
                Debug.Log("Packet");
                Hp--;
                if(Hp <= 0) {
                    ExpDrop();
                    Destroy(gameObject);
                }
                break;

            case "Wifi" :
                isWifi = true;
                wifiSp = other.gameObject.GetComponent<SpriteRenderer>();
                Debug.Log("Wifi");
                rb.constraints |= RigidbodyConstraints2D.FreezePositionY;
                rb.velocity = new Vector2((wifiSp.flipX == true ? transform.right.x : -transform.right.x) * 4f, 0);
                Hp--;
                if(Hp <= 0) {
                    ExpDrop();
                    Destroy(gameObject);
                }
                StartCoroutine(DelayWifi());
                break;

            case "BlueTooth" :
                if(other.gameObject.transform.IsChildOf(this.gameObject.transform)) {
                    Debug.Log("BlueTooth");
                    Hp--;
                    if(Hp <= 0) {
                        ExpDrop();
                        Destroy(gameObject);
                    }
                }
                break;

        }   
    }

    IEnumerator DelayWifi() {
        yield return new WaitForSeconds(0.5f);

        isWifi = false;
        rb.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
    }

    // 수정된 플립 함수
    void Flip(Vector2 movement) {
        if (movement.x > 0) {
            sp.flipX = false; // 오른쪽으로 갈 때는 flipX를 false로 설정
        } else if (movement.x < 0) {
            sp.flipX = true; // 왼쪽으로 갈 때는 flipX를 true로 설정
        }
    }

    void ExpDrop() {
        for(int i = 0; i < ExpValue; i++) {
            plusVectorX = Random.Range(-0.1f, 0.1f);
            plusVectorY = Random.Range(-0.1f, 0.1f);

            Exp = Instantiate(ExpPrefab, new Vector2(transform.position.x + plusVectorX, transform.position.y + plusVectorY), Quaternion.identity);
            Exp.transform.parent = ExpNode.transform;
        }
    }
}

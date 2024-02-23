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
    public BoxCollider2D box2D;
    public FireWall fireWall;
    public PlayerMove playerMove;

    public SpriteRenderer wifiSp;

    public float speed;
    public float Hp;
    public int ExpValue;
    public int poisonCount = 3;

    public float plusVectorX;
    public float plusVectorY;

    public bool isWifi = false;
    public bool isFireWallAllow = true;
    public bool isBig = false;

    void Start() {
        FindPlayer();
        ExpNode = GameObject.FindWithTag("ExpNode");
        if(isBig) {
            Vector3 currentScale = transform.localScale;

            currentScale.x *= 5f;
            currentScale.y *= 5f;

            transform.localScale = currentScale;

            Hp *= 2;
            speed *= 1.5f;
            ExpValue *= 2;
        }
    }

    void Update() {
        followPlayer();
    }

    void FindPlayer() {
        Player = GameObject.FindWithTag("Player");
        playerMove = Player.GetComponent<PlayerMove>();
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

            case "Firewall" :
                if(isFireWallAllow) {
                    fireWall = other.gameObject.GetComponent<FireWall>();
                    Hp -= fireWall.power;
                    if(Hp <= 0) {
                        ExpDrop();
                        Destroy(gameObject);
                    }
                    isFireWallAllow = false;
                    StartCoroutine(DelayFirewall(fireWall.powerDelay));
                }
                break;

            case "TrashBomb" :
                Hp--;
                if(Hp <= 0) {
                    ExpDrop();
                    Destroy(gameObject);
                }
                break;

            case "Pill" :
                Hp--;
                if(Hp <= 0) {
                    ExpDrop();
                    Destroy(gameObject);
                }
                break;

            case "Arrow" :
                Hp--;
                if(Hp <= 0) {
                    ExpDrop();
                    Destroy(gameObject);
                }
                break;

            case "Spin" :
                StartCoroutine(Poison());
                break;
        }  
    }

    void OnTriggerStay2D(Collider2D other) {
        switch(LayerMask.LayerToName(other.gameObject.layer)) { 
            case "Firewall" :
                if(isFireWallAllow) {
                    fireWall = other.gameObject.GetComponent<FireWall>();
                    Hp -= fireWall.power;
                    if(Hp <= 0) {
                        ExpDrop();
                        Destroy(gameObject);
                    }
                    isFireWallAllow = false;
                    StartCoroutine(DelayFirewall(fireWall.powerDelay));
                }
                break;
        } 
    }



    IEnumerator DelayWifi() {
        yield return new WaitForSeconds(0.5f);

        isWifi = false;
        rb.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
    }

    IEnumerator DelayFirewall(float timer) {
        yield return new WaitForSeconds(timer);

        isFireWallAllow = true;
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
        box2D.enabled = false;
        for(int i = 0; i < ExpValue; i++) {
            plusVectorX = Random.Range(-0.1f, 0.1f);
            plusVectorY = Random.Range(-0.1f, 0.1f);

            Exp = Instantiate(ExpPrefab, new Vector2(transform.position.x + plusVectorX, transform.position.y + plusVectorY), Quaternion.identity);
            Exp.transform.parent = ExpNode.transform;
        }
    }

    IEnumerator Poison() {
        Hp -= playerMove.spinDamage;
        poisonCount--;

        yield return new WaitForSeconds(1.0f);

        if(poisonCount != 0) {
            StartCoroutine(Poison());
        }
        else {
            poisonCount = 3;
        }
    }
}

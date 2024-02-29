using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour {

    public float minX = -25f; // X 축 최소값
    public float maxX = 25f;  // X 축 최대값
    public float minY = -25f; // Y 축 최소값
    public float maxY = 25f;  // Y 축 최대값

    Vector2 movement = new Vector2();

    public GameObject canvas;
    public Text text;
    public GameObject endPannel;

    public float timer = 0f;
    public string timer_string = "0";
    public string timer_m = null;
    public string timer_s = null;
    public int[] cardLevel = new int[8];

    public float Hp = 9f;
    public int maxHp = 10;
    public float Exp = 0.0f;
    public float ExpUp = 1.0f;
    public float ExpTimes = 1.0f;
    public float moveSpeed = 5.0f;
    public float unHitTime = 1.0f;
    public float attackSpeed = 1.0f;
    public float hpHeal = 1.0f;
    public float spinDamage = 1.0f;

    public bool isFacingRight;
    public bool isMoveAllow = true;
    public bool isHit = false;

    public Rigidbody2D rb;
    public SpriteRenderer sp;
    public BoxCollider2D box2D;
    public Card card;
    public Canvas canvasComponent;
    public Animator anim;

    public Wifi_Attack wifi_Attack;
    public Packet_Attack packet_Attack;
    public BlueTooth_Attack blueTooth_Attack;
    public Arrow_Attack arrow_Attack;
    public Spin_Item spin_Item;
    public TrashBomb_Attack trashBomb_Attack;
    public FireWall fireWall;
    public Pill_Attack pill_Attack;


    public PillCircle pillCircle;

    public GameObject Wifi_Weapon;
    public GameObject Ddos_Weapon;
    public GameObject BlueTooth_Weapon;
    public GameObject Arrow_Weapon;
    public GameObject Bug_Weapon;
    public GameObject TrashCan_Weapon;
    public GameObject Firewall_Weapon;
    public GameObject Pill_Weapon;

    public GameObject Enemy_Node;
    public GameObject hpBar;
    public Hp_Bar hp_Bar;

    public GameObject isWifi;
    public GameObject isDdos;
    public GameObject isBlue;
    public GameObject isArrow;
    public GameObject isBug;
    public GameObject isTrash;
    public GameObject isFirewall;
    public GameObject isPill;

    void Awake() {
        isFacingRight = true;
        InvokeRepeating("Heal", 0.1f, 10f);
        StartTimer();
    }

    void Start() {
        Array.Fill(cardLevel, 0);
        Time.timeScale = 0f;
        anim.Play("Player");
    }

    void Update() {
        if(isMoveAllow) {
            Player_Anim();
        }

        if(Hp <= 0f || timer >= 1200f) {
            StartCoroutine(Ending());
        }


        if(((int)(timer / 60f)) < 10) {
            timer_m = "0";
        }
        else {
            timer_m = null;
        }
        
        if(((int)(timer % 60f)) < 10) {
            timer_s = "0";
        }
        else {
            timer_s = null;
        }

        timer_string = timer_m + ((int)(timer / 60f)).ToString() + " : " +  timer_s + ((int)(timer % 60f)).ToString();
        text.text = timer_string;
    }

    void FixedUpdate() {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement.Normalize();
        if(isMoveAllow) {
            rb.velocity = movement * moveSpeed;
        }

        hp_Bar.CheckHp();

        
        Vector2 currentPosition = rb.position;
        currentPosition.x = Mathf.Clamp(currentPosition.x, minX, maxX);
        currentPosition.y = Mathf.Clamp(currentPosition.y, minY, maxY);
        rb.position = currentPosition;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void OnCollisionEnter2D(Collision2D other) {
        if(LayerMask.LayerToName(other.gameObject.layer) == "Wall") {
            Debug.Log("Wall");
        }         
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(LayerMask.LayerToName(other.gameObject.layer) == "Enemy" && !isHit) {
            Hp--;
            StartCoroutine(UnHit());
        }
        if(LayerMask.LayerToName(other.gameObject.layer) == "Exp") {
            Exp += ExpTimes;
            if(Exp == ExpUp) {
                ExpUp += 4;
                Exp = 0;
                card.CardInstantiate();
                canvasComponent.renderMode = RenderMode.ScreenSpaceCamera;
            }
        }
    }

    void OnTriggerStay2D(Collider2D other) {
        if(LayerMask.LayerToName(other.gameObject.layer) == "Enemy" && !isHit) {
            Hp--;
            StartCoroutine(UnHit());
        }
        if(LayerMask.LayerToName(other.gameObject.layer) == "Exp") {
            Exp += ExpTimes;
            if(Exp == ExpUp) {
                ExpUp += 4;
                Exp = 0;
                card.CardInstantiate();
                canvasComponent.renderMode = RenderMode.ScreenSpaceCamera;
            }
        }

    }

    IEnumerator UnHit() {
        Color color = sp.color;
        color.a = 0.5f;
        sp.color = color;
        isHit = true;

        yield return new WaitForSeconds(unHitTime);

        color = sp.color;
        color.a = 1.0f;
        sp.color = color;
        isHit = false;
    }

    public void TagCheck(string tag) {
        switch(tag) {
            case "arrowCard" : //0

                if(cardLevel[0] < 3) {
                    cardLevel[0]++;
                }
                
                if(isArrow == null) {
                    isArrow = Instantiate(Arrow_Weapon, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                    isArrow.transform.parent = this.transform;
                    arrow_Attack = isArrow.GetComponent<Arrow_Attack>();
                }
                else {
                    arrow_Attack.count++;
                }
                break;

            case "bluetoothCard" : //1

                if(cardLevel[1] < 3) {
                    cardLevel[1]++;
                }

                if(isBlue == null) {
                    isBlue = Instantiate(BlueTooth_Weapon, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                    isBlue.transform.parent = this.transform;
                    blueTooth_Attack = isBlue.GetComponent<BlueTooth_Attack>();
                }
                else {
                    blueTooth_Attack.maxCount++;
                }
                break;

            case "bugCard" : //2

                if(cardLevel[2] < 3) {
                    cardLevel[2]++;
                }

                if(isBug == null) {
                    isBug = Instantiate(Bug_Weapon, new Vector2(transform.position.x + 0.4f, transform.position.y + 0.5f), Quaternion.identity);
                    isBug.transform.parent = this.transform;
                    spin_Item = isBug.GetComponent<Spin_Item>();
                }
                else {
                    Debug.Log("Upgrade");
                }
                break;

            case "chargeCard" : 
                hpHeal += 0.1f;
                break;

            case "cpuCard" :
                float b;

                attackSpeed += 0.1f;
                b = 1;

                while(attackSpeed * b < 1) {
                    b -= 0.01f;
                }
                b += 0.01f;

                if(isWifi != null) {
                    wifi_Attack.Upgrade(b);
                }

                if(isDdos != null) {
                    packet_Attack.Upgrade(b);
                }

                if(isBlue != null) {
                    blueTooth_Attack.Upgrade(b);
                }

                if(isArrow != null) {
                    arrow_Attack.Upgrade(b);
                }

                if(isBug != null) {
                    spin_Item.Upgrade(b);
                }

                if(isTrash != null) {
                    trashBomb_Attack.Upgrade(b);
                }

                if(isFirewall != null) {
                    fireWall.Upgrade(b);
                }

                if(isPill != null) {
                    pill_Attack.Upgrade(0.2f);
                }
                break;

            case "ctrlzCard" : 
                if(Hp < maxHp - 3) {
                    Hp += 3;
                }
                else {
                    Hp = maxHp;
                }
                break;

            case "ddosCard" : //3

                if(cardLevel[3] < 3) {
                    cardLevel[3]++;
                }

                if(isDdos == null) {
                    isDdos = Instantiate(Ddos_Weapon, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                    isDdos.transform.parent = this.transform;
                    packet_Attack = isDdos.GetComponent<Packet_Attack>();
                }
                else {
                    packet_Attack.AttackCount++;
                }
                break;

            case "firewallCard" : //4

                if(cardLevel[4] < 3) {
                    cardLevel[4]++;
                }

                if(isFirewall == null) {
                    isFirewall = Instantiate(Firewall_Weapon, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                    isFirewall.transform.parent = this.transform;
                    fireWall = isFirewall.GetComponent<FireWall>();
                }
                else {
                    fireWall.sizeUp();
                }
                break;

            case "formatCard" :
                if (Enemy_Node != null) {
                    foreach (Transform child in Enemy_Node.transform) {
                        Destroy(child.gameObject);
                    }
                }
                break;

            case "hddCard" :
                maxHp += 1;
                Hp++;
                break;

            case "lagCard" :
                unHitTime += 0.1f;
                break;

            case "overflowCard" :
                ExpTimes += 0.1f;
                break;

            case "ramCard" :
                moveSpeed += 0.1f;
                break;

            case "recyclebinCard" : //5

                if(cardLevel[5] < 3) {
                    cardLevel[5]++;
                }

                if(isTrash == null) {
                    isTrash = Instantiate(TrashCan_Weapon, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                    isTrash.transform.parent = this.transform;
                    trashBomb_Attack = isTrash.GetComponent<TrashBomb_Attack>();
                }
                else {
                    trashBomb_Attack.AttackCount++;
                }
                break;

            case "wifiCard" : //6

                if(cardLevel[6] < 3) {
                    cardLevel[6]++;
                }

                if(isWifi == null) {
                    isWifi = Instantiate(Wifi_Weapon, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                    isWifi.transform.parent = this.transform;
                    wifi_Attack = isWifi.GetComponent<Wifi_Attack>();
                }
                else {
                    wifi_Attack.sizeUpgrade();
                }
                break;

            case "pillCard" : //7

                if(cardLevel[7] < 3) {
                    cardLevel[7]++;
                }

                if(isPill == null) {
                    isPill = Instantiate(Pill_Weapon, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                    isPill.transform.parent = this.transform;
                    pill_Attack = isPill.GetComponent<Pill_Attack>();
                }
                else {
                    pill_Attack.Pill_Instantiate();
                }
                break;
        }
        canvasComponent.renderMode = RenderMode.ScreenSpaceOverlay;
    }

    void Heal() {
        if(Hp < maxHp) {
            Hp += hpHeal;
        }
    }


    public void StartTimer() {
        Time.timeScale = 1f;
        InvokeRepeating("Timer", 0.1f, 1f);
    }

    public void Timer() {
        timer += 1f;
    }

    public void PausedTimer() {
        Time.timeScale = 0f;
        CancelInvoke("Timer");
    }

    void Player_Anim() {
        if(movement.x == 0) {
            if(movement.y < 0) {
                anim.Play("Player_Down");
            }
            else if(movement.y > 0) {
                anim.Play("Player_Up");
            }
            else if(movement.y == 0) {
                anim.Play("Player");
            }
        }
        else if(movement.x < 0) {
            if(movement.y == 0) {
                anim.Play("Player_Left");
            }
            else if(movement.y < 0) {
                anim.Play("Player_DL");
            }
            else if(movement.y > 0) {
                anim.Play("Player_UL");
            }
        }
        else if(movement.x > 0) {
            if(movement.y == 0) {
                anim.Play("Player_Right");
            }
            else if(movement.y < 0) {
                anim.Play("Player_DR");
            }
            else if(movement.y > 0) {
                anim.Play("Player_UR");
            }
        }
    }

    IEnumerator Ending() {
        rb.velocity = Vector2.zero;
        isMoveAllow = false;
        anim.Play("Player_Death");

        yield return new WaitForSeconds(2.1f);

        endPannel.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }
}
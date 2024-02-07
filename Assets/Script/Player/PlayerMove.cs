using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    Vector2 movement = new Vector2();

    public GameObject canvas;

    public int Hp = 10;
    public int maxHp = 10;
    public float Exp = 0.0f;
    public float ExpUp = 1.0f;
    public float ExpTimes = 1.0f;
    public float moveSpeed = 5.0f;
    public float unHitTime = 1.0f;
    public float attackSpeed = 1.0f;
    public float hpHeal = 1.0f;

    public bool isFacingRight;
    public bool isHit = false;

    public Rigidbody2D rb;
    public SpriteRenderer sp;
    public BoxCollider2D box2D;
    public Card card;

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
        TagCheck("wifiCard");
        TagCheck("arrowCard");
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.P)) {
            TagCheck("wifiCard");
        }
        Flip();
    }

    void FixedUpdate() {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement.Normalize();
        rb.velocity = movement * moveSpeed;
    }

    void Flip() {
        if(movement.x > 0 && !isFacingRight) {
            sp.flipX = !sp.flipX;
            isFacingRight = !isFacingRight;
        }
        else if(movement.x < 0 && isFacingRight) {
            sp.flipX = !sp.flipX;
            isFacingRight = !isFacingRight;
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(LayerMask.LayerToName(other.gameObject.layer) == "Enemy" && !isHit) {
            Hp--;
            StartCoroutine(UnHit());
        }
        if(LayerMask.LayerToName(other.gameObject.layer) == "Exp") {
            Exp++;
            if(Exp == ExpUp) {
                ExpUp += 4;
                Exp = 0;
                card.CardInstantiate();
            }
        }
    }

    void OnTriggerStay2D(Collider2D other) {
        if(LayerMask.LayerToName(other.gameObject.layer) == "Enemy" && !isHit) {
            Hp--;
            StartCoroutine(UnHit());
        }
        if(LayerMask.LayerToName(other.gameObject.layer) == "Exp") {
            Exp++;
            if(Exp == ExpUp) {
                ExpUp += 4;
                Exp = 0;
                card.CardInstantiate();
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
            case "arrowCard" :
                if(isArrow == null) {
                    isArrow = Instantiate(Arrow_Weapon, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                    isArrow.transform.parent = this.transform;
                    arrow_Attack = isArrow.GetComponent<Arrow_Attack>();
                }
                else {
                    Debug.Log("Upgrade");
                }
                break;

            case "bluetoothCard" :
                if(isBlue == null) {
                    isBlue = Instantiate(BlueTooth_Weapon, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                    isBlue.transform.parent = this.transform;
                    blueTooth_Attack = isBlue.GetComponent<BlueTooth_Attack>();
                }
                else {
                    Debug.Log("Upgrade");
                }
                break;

            case "bugCard" :
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

            case "ddosCard" :
                if(isDdos == null) {
                    isDdos = Instantiate(Ddos_Weapon, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                    isDdos.transform.parent = this.transform;
                    packet_Attack = isDdos.GetComponent<Packet_Attack>();
                }
                else {
                    Debug.Log("Upgrade");
                }
                break;

            case "firewallCard" :
                if(isFirewall == null) {
                    isFirewall = Instantiate(Firewall_Weapon, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                    isFirewall.transform.parent = this.transform;
                    fireWall = isFirewall.GetComponent<FireWall>();
                }
                else {
                    Debug.Log("Upgrade");
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

            case "recyclebinCard" :
                isTrash = Instantiate(Firewall_Weapon, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                isTrash.transform.parent = this.transform;
                trashBomb_Attack = isTrash.GetComponent<TrashBomb_Attack>();
                break;

            case "wifiCard" :
                if(isWifi == null) {
                    isWifi = Instantiate(Wifi_Weapon, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                    isWifi.transform.parent = this.transform;
                    wifi_Attack = isWifi.GetComponent<Wifi_Attack>();
                }
                else {
                    wifi_Attack.sizeUpgrade();
                }
                break;

            case "pillCard" :
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
    }
}
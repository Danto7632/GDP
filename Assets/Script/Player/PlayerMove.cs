using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    Vector2 movement = new Vector2();

    public GameObject canvas;

    public int Hp = 10;
    public int Exp = 0;
    public int ExpUp = 4;
    public float moveSpeed = 5.0f;
    public float unHitTime = 1.0f;

    public bool isFacingRight;
    public bool isHit = false;

    public Rigidbody2D rb;
    public SpriteRenderer sp;
    public BoxCollider2D box2D;
    public Card card;

    public GameObject Wifi_Weapon;
    public GameObject Ddos_Weapon;
    public GameObject BlueTooth_Weapon;
    public GameObject Arrow_Weapon;
    public GameObject Bug_Weapon;
    public GameObject TrashCan_Weapon;
    public GameObject Firewall_Weapon;
    public GameObject Pill_Weapon;

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
    }

    void Update() {
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
                }
                else {
                    Debug.Log("Upgrade");
                }
                break;

            case "bluetoothCard" :
                if(isBlue == null) {
                    isBlue = Instantiate(BlueTooth_Weapon, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                    isBlue.transform.parent = this.transform;
                }
                else {
                    Debug.Log("Upgrade");
                }
                break;

            case "bugCard" :
                if(isBug == null) {
                    isBug = Instantiate(Bug_Weapon, new Vector2(transform.position.x + 0.4f, transform.position.y + 0.5f), Quaternion.identity);
                    isBug.transform.parent = this.transform;
                }
                else {
                    Debug.Log("Upgrade");
                }
                break;

            case "chargeCard" : 
                Debug.Log("Upgrade");
                break;

            case "cpuCard" :
                Debug.Log("Upgrade");
                break;

            case "ctrlzCard" : 
                Debug.Log("Upgrade");
                break;

            case "ddosCard" :
                if(isDdos == null) {
                    isDdos = Instantiate(Ddos_Weapon, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                    isDdos.transform.parent = this.transform;
                }
                else {
                    Debug.Log("Upgrade");
                }
                break;

            case "firewallCard" :
                if(isFirewall == null) {
                    isFirewall = Instantiate(Firewall_Weapon, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                    isFirewall.transform.parent = this.transform;
                }
                else {
                    Debug.Log("Upgrade");
                }
                break;

            case "formatCard" :
                Debug.Log("Upgrade");
                break;

            case "hddCard" :
                Debug.Log("Upgrade");
                break;

            case "lagCard" :
                Debug.Log("Upgrade");
                break;

            case "overflowCard" :
                Debug.Log("Upgrade");
                break;

            case "ramCard" :
                Debug.Log("Upgrade");
                break;

            case "recyclebinCard" :
                Debug.Log("Upgrade");
                break;

            case "wifiCard" :
                if(isWifi == null) {
                    isWifi = Instantiate(Wifi_Weapon, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                    isWifi.transform.parent = this.transform;
                }
                else {
                    Debug.Log("Upgrade");
                }
                break;
        }
    }
}
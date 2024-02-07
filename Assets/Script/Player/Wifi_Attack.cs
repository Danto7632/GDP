using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wifi_Attack : MonoBehaviour {
    public GameObject WifiPrefab;
    public GameObject Wifi;
    public GameObject Player;

    public PlayerMove playerMove;
    public SpriteRenderer sp;

    public float AttackSpeed = 4.0f;
    public float sizeUp = 1.0f;

    public bool isR = false;

    void Start() {
        Player = transform.parent.gameObject;
        playerMove = Player.GetComponent<PlayerMove>();
        StartCoroutine(Attack());
    }

    IEnumerator Attack() {
        Wifi = Instantiate(WifiPrefab, new Vector2(isR == true ? (transform.position.x + 0.9f) : (transform.position.x - 0.9f), transform.position.y), Quaternion.identity);
        Wifi.transform.parent = this.transform;

        sp = Wifi.GetComponent<SpriteRenderer>();
        sp.flipX = sp.flipX = (isR == true ? true : false);
        Wifi.transform.localScale *= sizeUp;
    
        yield return new WaitForSeconds(AttackSpeed);
        isR = !isR;

        StartCoroutine(Attack());
    }

    public void Upgrade(float b) {
        AttackSpeed *= b;
    }

    public void sizeUpgrade() {
        sizeUp *= 1.2f;
    }
}

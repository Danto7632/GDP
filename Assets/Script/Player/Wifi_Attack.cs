using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wifi_Attack : MonoBehaviour {
    public GameObject WifiPrefab;
    public GameObject Wifi;
    public GameObject Player;

    public WifiFan wifiFan;
    public PlayerMove playerMove;
    public SpriteRenderer sp;

    public float AttackSpeed = 4.0f;

    void Start() {
        playerMove = Player.GetComponent<PlayerMove>();
        StartCoroutine(Attack());
    }

    IEnumerator Attack() {
        Wifi = Instantiate(WifiPrefab, new Vector2(playerMove.isFacingRight == true ? (transform.position.x + 0.9f) : (transform.position.x - 0.9f), transform.position.y), Quaternion.identity);
        Wifi.transform.parent = this.transform;

        sp = Wifi.GetComponent<SpriteRenderer>();
        sp.flipX = (playerMove.isFacingRight == true ? true : false);
    
        yield return new WaitForSeconds(AttackSpeed);

        StartCoroutine(Attack());
    }
}

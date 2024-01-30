using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    Vector2 movement = new Vector2();

    public int Hp = 10;
    public int Exp = 0;
    public float moveSpeed = 5.0f;
    public float unHitTime = 1.0f;

    public bool isFacingRight;
    public bool isHit = false;

    public Rigidbody2D rb;
    public SpriteRenderer sp;
    public BoxCollider2D box2D;

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
        }
    }

    void OnTriggerStay2D(Collider2D other) {
        if(LayerMask.LayerToName(other.gameObject.layer) == "Enemy" && !isHit) {
            Hp--;
            StartCoroutine(UnHit());
        }
        if(LayerMask.LayerToName(other.gameObject.layer) == "Exp") {
            Exp++;
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
}














//
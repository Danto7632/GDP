using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    Vector2 movement = new Vector2();


    public int Hp = 10;
    public float moveSpeed = 5.0f;
    public float unHitTime = 1.0f;

    public bool isFacingRight;

    public Rigidbody2D rb;
    public SpriteRenderer sp;

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
}














//
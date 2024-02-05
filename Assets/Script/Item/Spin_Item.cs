using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin_Item : MonoBehaviour {
    public SpriteRenderer sp;

    public GameObject spinPrefab;
    public GameObject Spin;

    public Spin spin;

    public float AttackSpeed = 2.0f;
    public float angle;

    public bool isFacingRight = false;

    void Start() {
        StartCoroutine(RepeatCoroutine());
    }

    void Attack() {
        Spin = Instantiate(spinPrefab, transform.position, Quaternion.identity);
        StartCoroutine(RepeatCoroutine());
    }

    public void Flip() {
        if(angle >= 360) {
            angle -= 360;
        }
        
        Debug.Log(angle);

        if((angle > 90 && angle < 270) && isFacingRight) { //90에서 270까지
            sp.flipX = !sp.flipX;
            isFacingRight = !isFacingRight;
        }
        else if((angle <= 90 && angle >= 270) && !isFacingRight) { //90 > x > 270
            sp.flipX = !sp.flipX;
            isFacingRight = !isFacingRight;
        }
    }

    IEnumerator RepeatCoroutine() {
        yield return new WaitForSeconds(AttackSpeed);
        Attack();
    }
}
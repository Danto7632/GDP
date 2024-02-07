using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMouse : MonoBehaviour {
    public Vector3 mousePosition;
    public Vector2 direction;

    public Rigidbody2D rb;

    void Start() {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        direction = (mousePosition - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);

        Destroy(gameObject, 5f);
    }

    void Update() {
        goForward();
    }

    void goForward() {
        rb.AddForce(direction * 0.01f, ForceMode2D.Impulse);
    }
}
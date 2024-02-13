using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMouse : MonoBehaviour {
    public Vector3 mousePosition;
    public Vector2 direction;
    public int count;
    public int number;
    public float angle;

    public Rigidbody2D rb;

    void Start() {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        direction = (mousePosition - transform.position).normalized;

        angle = 360f / count * number;
        direction = Quaternion.Euler(0, 0, angle) * direction;
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour {

    public float spinSpeed = 10f;
    public Vector2 flyingDirection;
    public float currentRotation;
    public int RandomAngle;
    public int currentAngle;

    public Rigidbody2D rb;

    void Start() {
       RandomAngle = Random.Range(0, 360);

       transform.rotation = Quaternion.Euler(0f, 0f, RandomAngle + 135);
    }

    void Update() {
        currentRotation = transform.rotation.eulerAngles.z;

        flyingDirection = Quaternion.Euler(0f, 0f, currentRotation) * Vector2.up;
        rb.velocity = flyingDirection * spinSpeed;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillCircle : MonoBehaviour {
    public GameObject Player;

    public float radius = 1.2f;
    public float rotationSpeed = 100f;
    public float angle = 0f;

    public float radians;
    public float x;
    public float y;

    public Vector3 center;

    void Start() {
        Player = transform.parent.gameObject;
    }

    void Update() {
        if(transform.parent != null) {
            center = transform.parent.position;
        }

        radians = Mathf.Deg2Rad * angle;

        x = center.x + Mathf.Cos(radians) * radius;
        y = center.y + Mathf.Sin(radians) * radius;

        transform.position = new Vector3(x, y, 0f);

        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);

        angle += rotationSpeed * Time.deltaTime;

        if(angle >= 360f) {
            angle = 0f;
        }
    }
}
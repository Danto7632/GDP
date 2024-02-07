using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWall : MonoBehaviour {
    public int power = 1;
    public float powerDelay = 1f;
    public float rotationSpeed = 500f;

    void Update() {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }

    public void Upgrade(float b) {
        powerDelay *= b;
    }
}
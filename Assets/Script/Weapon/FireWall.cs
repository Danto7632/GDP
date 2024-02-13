using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWall : MonoBehaviour {
    public int power = 1;
    public float powerDelay = 1f;
    public float rotationSpeed = 500f;

    public Vector3 currentScale;

    void Update() {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }

    public void Upgrade(float b) {
        powerDelay *= b;
    }

    public void sizeUp() {
        currentScale = transform.localScale;
        transform.localScale = new Vector3(currentScale.x * 1.2f, currentScale.y * 1.2f, currentScale.z * 1.2f);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin_Item : MonoBehaviour {
    public GameObject spinPrefab;
    public GameObject Spin;

    public float AttackSpeed = 2.0f;

    void Start() {
        StartCoroutine(RepeatCoroutine());
    }

    void Attack() {
        Spin = Instantiate(spinPrefab, transform.position, Quaternion.identity);
        StartCoroutine(RepeatCoroutine());
    }

    IEnumerator RepeatCoroutine() {
        yield return new WaitForSeconds(AttackSpeed);
        Attack();
    }
}
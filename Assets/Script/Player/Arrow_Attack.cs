using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow_Attack : MonoBehaviour {
    public GameObject ArrowPrefab;
    public GameObject Arrow;

    public float AttackSpeed = 2.0f;

    void Start() {
        StartCoroutine(RepeatCoroutine());
    }

    void Attack() {
        Arrow = Instantiate(ArrowPrefab, transform.position, Quaternion.identity);
        StartCoroutine(RepeatCoroutine());
    }

    IEnumerator RepeatCoroutine() {
        yield return new WaitForSeconds(AttackSpeed);
        Attack();
    }

    public void Upgrade(float b) {
        AttackSpeed *= b;
    }
}
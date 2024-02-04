using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow_Attack : MonoBehaviour {
    public GameObject ArrowPrefab;
    public GameObject Arrow;
    public GameObject targetingEnemy;

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
}
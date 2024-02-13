using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow_Attack : MonoBehaviour {
    public GameObject ArrowPrefab;
    public GameObject Arrow;
    public ArrowMouse arrowMouse;

    public int count = 1;
    public float AttackSpeed = 2.0f;

    void Start() {
        StartCoroutine(RepeatCoroutine());
    }

    void Attack() {
        for(int i = 1; i <= count; i++) {
            Arrow = Instantiate(ArrowPrefab, transform.position, Quaternion.identity);
            arrowMouse = Arrow.GetComponent<ArrowMouse>();
            arrowMouse.count = count;
            arrowMouse.number = i;
        }
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
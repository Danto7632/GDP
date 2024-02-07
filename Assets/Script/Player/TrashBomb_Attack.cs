using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBomb_Attack : MonoBehaviour {
    public GameObject TrashBombPrefab;
    public GameObject TrashBomb;
    public TrashBomb trashBomb;   

    public float AttackSpeed = 2.0f;

    public float plusVectorX;
    public float plusVectorY;

    void Start() {
        StartCoroutine(RepeatCoroutine());
    }

    void Attack(float x, float y) {
        TrashBomb = Instantiate(TrashBombPrefab, new Vector2(transform.position.x + x + 2.5f, transform.position.y + y + 2.5f), Quaternion.Euler(0f, 0f, 150f));
        trashBomb = TrashBomb.GetComponent<TrashBomb>();
        Vector2 Position = new Vector2(transform.position.x + x, transform.position.y + y);
        StartCoroutine(RepeatCoroutine());
    }

    IEnumerator RepeatCoroutine() {
        plusVectorX = Random.Range(-2.5f, 2.5f);
        plusVectorY = Random.Range(-2.5f, 2.5f);

        yield return new WaitForSeconds(AttackSpeed);
        Attack(plusVectorX, plusVectorY);
    }

    public void Upgrade(float b) {
        AttackSpeed *= b;
    }
}
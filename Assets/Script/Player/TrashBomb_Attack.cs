using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBomb_Attack : MonoBehaviour {
    public GameObject TrashBombPrefab;
    public GameObject TrashBomb;
    public TrashBomb trashBomb;   

    public float AttackSpeed = 2.0f;
    public float AttackCount = 1.0f;

    public float x;
    public float y;

    void Start() {
        StartCoroutine(RepeatCoroutine());
    }

    IEnumerator RepeatCoroutine() {
        for(int i = 0; i < AttackCount; i++) {
            x = Random.Range(-2.5f, 2.5f);
            y = Random.Range(-2.5f, 2.5f);

            TrashBomb = Instantiate(TrashBombPrefab, new Vector2(transform.position.x + x + 2.5f, transform.position.y + y + 2.5f), Quaternion.Euler(0f, 0f, 150f));
            trashBomb = TrashBomb.GetComponent<TrashBomb>();
            Vector2 Position = new Vector2(transform.position.x + x, transform.position.y + y);
        }

        yield return new WaitForSeconds(AttackSpeed);

        StartCoroutine(RepeatCoroutine());
    }

    public void Upgrade(float b) {
        AttackSpeed *= b;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueTooth_Attack : MonoBehaviour {
    public GameObject BlueToothPrefab;
    public GameObject BlueTooth;
    public List<GameObject> targetingEnemies = new List<GameObject>();
    public BlueToothBolt blueToothBolt;   

    public int maxCount = 1;
    public float AttackSpeed = 2.0f;
    public float AttackCount = 1.0f;

    void Start() {
        StartCoroutine(RepeatCoroutine());
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(LayerMask.LayerToName(other.gameObject.layer) == "Enemy") {
            GameObject enemy = other.gameObject;
            if (!targetingEnemies.Contains(enemy)) {
                targetingEnemies.Add(enemy);
                Debug.Log("EnemyDetectionRange");
            }
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (LayerMask.LayerToName(other.gameObject.layer) == "Enemy") {
            GameObject enemy = other.gameObject;
            if (targetingEnemies.Contains(enemy)) {
                targetingEnemies.Remove(enemy);
            }
        }
    }

    void Attack(GameObject enemy) {
        BlueTooth = Instantiate(BlueToothPrefab, new Vector2(enemy.transform.position.x, enemy.transform.position.y + 0.75f), Quaternion.identity);
        BlueTooth.transform.parent = enemy.transform;
    }

    IEnumerator RepeatCoroutine() {
        yield return new WaitForSeconds(AttackSpeed);

        for(int i = 0; i < Mathf.Min(maxCount, targetingEnemies.Count); i++) {
            if(targetingEnemies[i] != null) {
                Attack(targetingEnemies[i]);
            }
        }

        StartCoroutine(RepeatCoroutine());
    }

    public void Upgrade(float b) {
        AttackSpeed *= b;
    }
}
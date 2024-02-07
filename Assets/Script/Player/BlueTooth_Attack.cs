using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueTooth_Attack : MonoBehaviour {
    public GameObject BlueToothPrefab;
    public GameObject BlueTooth;
    public GameObject targetingEnemy;
    public BlueToothBolt blueToothBolt;   

    public float AttackSpeed = 2.0f;

    void OnTriggerStay2D(Collider2D other) {
        if(targetingEnemy == null && LayerMask.LayerToName(other.gameObject.layer) == "Enemy") {
            targetingEnemy = other.gameObject;
            Debug.Log("EnemyDetectionRange");
            StartCoroutine(RepeatCoroutine(other));
    
        }
    }

    void Attack(Collider2D Enemy) {
        BlueTooth = Instantiate(BlueToothPrefab, new Vector2(Enemy.transform.position.x, Enemy.transform.position.y + 0.75f), Quaternion.identity);
        BlueTooth.transform.parent = Enemy.transform;
    }

    IEnumerator RepeatCoroutine(Collider2D Enemy) {
        while (Enemy != null) {
            yield return new WaitForSeconds(AttackSpeed);
            if (Enemy != null) {
                Attack(Enemy);
            }
        }
    }

    public void Upgrade(float b) {
        AttackSpeed *= b;
    }
}
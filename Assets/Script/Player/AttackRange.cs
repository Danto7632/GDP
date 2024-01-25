using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour {
    public GameObject WeaponPrefab;
    public GameObject Packet;

    public float AttackSpeed = 2.0f;

    void OnTriggerEnter2D(Collider2D other) {
        switch(LayerMask.LayerToName(other.gameObject.layer)) {    
            case "Enemy" :
                Debug.Log("EnemyDetectionRange");
                StartCoroutine(RepeatCoroutine(other));
                break;
        }
    }

    void Attack() {
        Packet = Instantiate(WeaponPrefab, this.transform.position, Quaternion.identity);
    }

    IEnumerator RepeatCoroutine(Collider2D Enemy) {
        while(Enemy.gameObject != null) {
            yield return new WaitForSeconds(AttackSpeed);
            Attack();
        }
    }
    
}

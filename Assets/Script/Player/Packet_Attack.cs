using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Packet_Attack : MonoBehaviour {
    public GameObject WeaponPrefab;
    public GameObject Packet;
    public GameObject targetingEnemy;
    public PacketBullet packetBullet;   

    public float AttackSpeed = 1.0f;

    void OnTriggerEnter2D(Collider2D other) {
        if(targetingEnemy == null && LayerMask.LayerToName(other.gameObject.layer) == "Enemy") {
            targetingEnemy = other.gameObject;
            Debug.Log("EnemyDetectionRange");
            StartCoroutine(RepeatCoroutine(other));
    
        }
    }

    void Attack(Collider2D Enemy) {
        Packet = Instantiate(WeaponPrefab, transform.position, Quaternion.identity);
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

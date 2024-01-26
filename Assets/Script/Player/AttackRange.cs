using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour {
    public GameObject WeaponPrefab;
    public GameObject Packet;
    public PacketBullet packetBullet;

    public float AttackSpeed = 2.0f;

    void OnTriggerEnter2D(Collider2D other) {
        switch(LayerMask.LayerToName(other.gameObject.layer)) {    
            case "Enemy" :
                Debug.Log("EnemyDetectionRange");
                StartCoroutine(RepeatCoroutine(other));
                break;
        }
    }

    void Attack(Collider2D Enemy) {
        Packet = Instantiate(WeaponPrefab, this.transform.position, Quaternion.identity);
        packetBullet = Packet.GetComponent<PacketBullet>();
        packetBullet.FindEnemy(Enemy);
    }

    IEnumerator RepeatCoroutine(Collider2D Enemy) {
        while(Enemy.gameObject != null) {
            yield return new WaitForSeconds(AttackSpeed);
            Attack(Enemy);
        }
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour {
    public GameObject WeaponPrefab;
    public GameObject Packet;
    public PacketBullet packetBullet;

    public float AttackSpeed = 1.0f;

    void OnTriggerEnter2D(Collider2D other) {
        switch(LayerMask.LayerToName(other.gameObject.layer)) {    
            case "Enemy" :
                Debug.Log("EnemyDetectionRange");
                StartCoroutine(RepeatCoroutine(other));
                break;
        }
    }

    void Attack(Collider2D Enemy) {
        Packet = Instantiate(WeaponPrefab, transform.position, Quaternion.identity);

        // Instantiate로 생성된 Packet에 PacketBullet 컴포넌트가 있는지 확인 후 가져오기
        packetBullet = Packet.GetComponent<PacketBullet>();
        if (packetBullet != null) {
            packetBullet.FindEnemy(Enemy);
        } else {
            Debug.LogError("PacketBullet component not found on the instantiated object.");
        }
    }

    IEnumerator RepeatCoroutine(Collider2D Enemy) {
        while (Enemy != null) {
            yield return new WaitForSeconds(AttackSpeed);
            if (Enemy != null)
                Attack(Enemy);
        }
    }
}

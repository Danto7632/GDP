using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour {

    public float spinSpeed = 10f;
    public int RandomAngle;
    public int currentAngle;

    public Rigidbody2D rb;

    void Start() {
       RandomAngle = Random.Range(0, 360);

       transform.rotation = Quaternion.Euler(0f, 0f, RandomAngle);
       currentAngle = RandomAngle + 315;
    }

    void Update() {
        // 현재 각도를 라디안으로 변환하여 방향 벡터 계산
        Vector2 direction = new Vector2(Mathf.Cos(currentAngle * Mathf.Deg2Rad), Mathf.Sin(currentAngle * Mathf.Deg2Rad));

        // Rigidbody2D의 속도 설정
        rb.velocity = direction * spinSpeed;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {
    public int[] spawnPoint = new int[16];
    public int spawnNumber;
    public int plusNumber;
    public int[] spawnRange = new int[2];

    public bool gameOver = false;
    public float spawnTime = 3.0f;

    public GameObject Enemy;

    void Start() {
        int seed = (int)System.DateTime.Now.Ticks;
        Random.InitState(seed);

        spawnNumber = Random.Range(0, 16);
        spawnNumber = 10;
        for(int i = 0; i < 10; i++) {
            SpawnEnemy();
        }
    }

    void SpawnEnemy() {
        spawnRange[0] = spawnNumber - 5;
        spawnRange[1] = spawnNumber + 5;

        for(int i = 0; i < 2; i++) {
            spawnRange[i] %= 16;
            if(spawnRange[i]< 0) {
                spawnRange[i] += 17;
            }
            Debug.Log(spawnPoint[spawnRange[i]]);
        }

        plusNumber = Random.Range(0, 8);

        Debug.Log("plusnumber : " + plusNumber);
        
        spawnNumber = (spawnRange[0] < spawnRange[1]) ? (spawnRange[1] + plusNumber) : (spawnRange[0] + plusNumber);

        if(spawnNumber > 15) {
            spawnNumber -= 16;
        }

        Debug.Log("새로 정해진 spawnNumber : " + spawnNumber);
    }


}
/*

X와 Y의 값을 받는다.
6.17     0.15

X와 Y의 값에 랜덤한 값을 증감 시킬 X1 Y1을 생성하여
X값과 Y값에 증감한다.




*/
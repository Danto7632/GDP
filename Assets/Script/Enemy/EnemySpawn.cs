using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {
    public int[] spawnPointIndex = new int[16];
    public int spawnNumber;
    public int plusNumber;
    public float plusVectorX;
    public float plusVectorY;
    public int[] spawnRange = new int[2];

    public bool gameOver = false;
    public float spawnTime = 3.0f;
    public float timer_Check = 300f;

    public int wave = 1;
    public int RandomModSpawn;

    public GameObject[] EnemyPrefab = new GameObject[6];
    public GameObject SpawnEnemies;
    public GameObject EnemyNode;

    public PlayerMove playerMove;

    public Dictionary<int, Vector2> coordinateDictionary = new Dictionary<int, Vector2>();

    void Start() {
        List<Vector2> coordinates = new List<Vector2>
        {
            new Vector2(-30, 30),
            new Vector2(-15, 30),
            new Vector2(0, 30),
            new Vector2(15, 30),
            new Vector2(30, 30),
            new Vector2(30, 15),
            new Vector2(30, 0),
            new Vector2(30, -15),
            new Vector2(30, -30),
            new Vector2(15, -30),
            new Vector2(0, -30),
            new Vector2(-15, -30),
            new Vector2(-30, -30),
            new Vector2(-30, -15),
            new Vector2(-30, 0),
            new Vector2(-30, 15)
        };

        // 값들을 Dictionary에 추가
        for (int i = 0; i < coordinates.Count; i++)
        {
            coordinateDictionary.Add(i, coordinates[i]);
        }

        int seed = (int)System.DateTime.Now.Ticks;
        Random.InitState(seed);

        spawnNumber = Random.Range(0, 16);
        spawnNumber = 10;
        
        StartCoroutine(RepeatCoroutine());
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.L)) {
            ALOTEnemy_Spawn();
        }
        if(playerMove.timer == timer_Check) {
            Debug.Log("wave");
            timer_Check += 300f;
            spawnTime -= 0.2f;
            wave++;
            if(wave == 6) {
                wave--;
            }
            ALOTEnemy_Spawn();
        }
    }

    void SpawnEnemy() {
        spawnRange[0] = spawnNumber - 5;
        spawnRange[1] = spawnNumber + 5;

        RandomModSpawn = Random.Range(0, wave + 1);

        for(int i = 0; i < 2; i++) {
            spawnRange[i] %= 16;
            if(spawnRange[i] < 0) {
                spawnRange[i] += 16; //확인필요
            }
            Debug.Log(spawnPointIndex[spawnRange[i]]);
        }

        plusNumber = Random.Range(0, 8);

        Debug.Log("plusnumber : " + plusNumber);
        
        spawnNumber = (spawnRange[0] < spawnRange[1]) ? (spawnRange[1] + plusNumber) : (spawnRange[0] + plusNumber);

        if(spawnNumber > 15) {
            spawnNumber -= 16;
        }

        Debug.Log("새로 정해진 spawnNumber : " + spawnNumber);
        Debug.Log("Value for Key " + spawnNumber + ": " + coordinateDictionary[spawnNumber]);

        plusVectorX = Random.Range(-7.5f, 7.5f);
        plusVectorY = Random.Range(-7.5f, 7.5f);
        
        

        SpawnEnemies = Instantiate(EnemyPrefab[RandomModSpawn], new Vector2(coordinateDictionary[spawnNumber].x + plusVectorX, coordinateDictionary[spawnNumber].y + plusVectorY), Quaternion.identity);
        SpawnEnemies.transform.parent = EnemyNode.transform;
    }

    IEnumerator RepeatCoroutine() {
        while (!gameOver) {
            yield return new WaitForSeconds(3f);
            SpawnEnemy();
        }
    }

    void ALOTEnemy_Spawn() {
        Debug.Log("Big wave");
        for(int i = 0; i < 3 * wave; i++) {
            SpawnEnemy();
        }
    }
}


/*

X와 Y의 값을 받는다.
6.17     0.15

X와 Y의 값에 랜덤한 값을 증감 시킬 X1 Y1을 생성하여
X값과 Y값에 증감한다.




*/
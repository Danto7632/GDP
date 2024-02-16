using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardSelect : MonoBehaviour {

    public Card card;
    public GameObject Player;
    public PlayerMove playerMove;

    public GameObject levelballPrefab;
    public GameObject nextballPrefab;

    public GameObject[] Ball = new GameObject[3];
    public GameObject nextBall;

    void Start() {
        Player = GameObject.FindWithTag("Player");
        playerMove = Player.GetComponent<PlayerMove>();
        levelCheck();
    }

    public void OnMouseDown() {
        card = transform.parent.gameObject.GetComponent<Card>();
        card.RemoveAllChildren();

        playerMove.TagCheck(this.tag);
    }

    void levelCheck() {
        switch(this.tag) {
            case "arrowCard" : //0
                CreateBall(playerMove.cardLevel[0]);
                break;
            case "bluetoothCard" : //1
                CreateBall(playerMove.cardLevel[1]);
                break;
            case "bugCard" : //2
                CreateBall(playerMove.cardLevel[2]);
                break;
            case "ddosCard" : //3
                CreateBall(playerMove.cardLevel[3]);
                break;
            case "firewallCard" : //4
                CreateBall(playerMove.cardLevel[4]);
                break;
            case "recyclebinCard" : //5
                CreateBall(playerMove.cardLevel[5]);
                break; 
            case "wifiCard" : //6
                CreateBall(playerMove.cardLevel[6]);
                break;
            case "pillCard" : //7
                CreateBall(playerMove.cardLevel[7]);
            break;
        }
    }

    Vector2 firstPosition = new Vector2(-2.15f, 0);
    Vector2 secondPosition = new Vector2(0, 0);
    Vector2 tHirdPosition = new Vector2(2.15f, 0);

    void CreateBall(int count) {
        switch(count) {
            case 0 :
                nextBall = Instantiate(nextballPrefab, firstPosition, Quaternion.identity);

                nextBall.transform.parent = this.gameObject.transform;

                nextBall.transform.localPosition = firstPosition;
                break;
            case 1 :
                nextBall = Instantiate(nextballPrefab, secondPosition, Quaternion.identity);
                Ball[0] = Instantiate(levelballPrefab, firstPosition, Quaternion.identity);

                nextBall.transform.parent = this.gameObject.transform;
                Ball[0].transform.parent = this.gameObject.transform;

                nextBall.transform.localPosition = secondPosition;
                Ball[0].transform.localPosition = firstPosition;
                break;
            case 2 :
                nextBall = Instantiate(nextballPrefab, tHirdPosition, Quaternion.identity);
                Ball[0] = Instantiate(levelballPrefab, secondPosition, Quaternion.identity);
                Ball[1] = Instantiate(levelballPrefab, firstPosition, Quaternion.identity);

                nextBall.transform.parent = this.gameObject.transform;
                Ball[0].transform.parent = this.gameObject.transform;
                Ball[1].transform.parent = this.gameObject.transform;

                nextBall.transform.localPosition = tHirdPosition;
                Ball[0].transform.localPosition = secondPosition;
                Ball[1].transform.localPosition = firstPosition;
                break;
            case 3 :
                Ball[0] = Instantiate(levelballPrefab, secondPosition, Quaternion.identity);
                Ball[1] = Instantiate(levelballPrefab, firstPosition, Quaternion.identity);
                Ball[2] = Instantiate(levelballPrefab, tHirdPosition, Quaternion.identity);

                Ball[0].transform.parent = this.gameObject.transform;
                Ball[1].transform.parent = this.gameObject.transform;
                Ball[2].transform.parent = this.gameObject.transform;

                Ball[0].transform.localPosition = secondPosition;
                Ball[1].transform.localPosition = firstPosition;
                Ball[2].transform.localPosition = tHirdPosition;
                break;
        }
    }
}
    
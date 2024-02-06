using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardSelect : MonoBehaviour {

    public Card card;
    public GameObject Player;
    public PlayerMove playerMove;

    void Start() {
        Player = GameObject.FindWithTag("Player");
        playerMove = Player.GetComponent<PlayerMove>();
    }

    public void OnMouseDown() {
        card = transform.parent.gameObject.GetComponent<Card>();
        card.RemoveAllChildren();

        playerMove.TagCheck(this.tag);
    }
}
    
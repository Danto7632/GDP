using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Select_Weapon : MonoBehaviour {
    public GameObject panel;
    public DeleteChild deleteChild;

    public GameObject Player;
    public PlayerMove playerMove;

    void Start() {
        playerMove = Player.GetComponent<PlayerMove>();
        deleteChild = panel.GetComponent<DeleteChild>();
    }

    public void Select() {
        playerMove.TagCheck(this.gameObject.tag);
        Time.timeScale = 1f;
        deleteChild.childDel();
    }
}

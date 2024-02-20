using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour {
    public GameObject Player;
    public PlayerMove playerMove;

    void Start() {
        playerMove = Player.GetComponent<PlayerMove>();
        this.gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hp_Bar : MonoBehaviour {
    public float currnetHp;
    public float maxHp;

    public GameObject Player;
    public PlayerMove playerMove;
    public Slider hpBar;

    public void CheckHp() {
        currnetHp = playerMove.Hp;
        maxHp = playerMove.maxHp;

        hpBar.maxValue = maxHp;
        hpBar.value = currnetHp;

        if(hpBar.value <= 0) {
            transform.Find("Fill Area").gameObject.SetActive(false);
        }
        else {  
            transform.Find("Fill Area").gameObject.SetActive(true);
        }
    }
}
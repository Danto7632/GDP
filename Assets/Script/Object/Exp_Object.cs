using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exp_Object : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D other) {
        if(LayerMask.LayerToName(other.gameObject.layer) == "Player") {
            Destroy(gameObject);
        }
    }
}
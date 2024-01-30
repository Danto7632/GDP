using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WifiFan : MonoBehaviour {
    public Animator animator;

    void Start() {
        Destroy(this.gameObject, animator.speed);
    }
}
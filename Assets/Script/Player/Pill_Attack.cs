using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pill_Attack : MonoBehaviour {
    public GameObject PillPrefab;
    public GameObject Pill;

    public PillCircle pillCircle;

    public float angleNum = 0;
    public float plusAngle = 0;

    void Start() {
        Pill_Instantiate();
    }

    public void Pill_Instantiate() {
        angleNum = 0;
        Pill = Instantiate(PillPrefab, transform.position, Quaternion.identity);
        Pill.transform.parent = this.gameObject.transform;
        if(transform.childCount > 1) {
            plusAngle = 360 / transform.childCount;
            for(int i = 0; i < transform.childCount; i++) {
                pillCircle = transform.GetChild(i).GetComponent<PillCircle>();
                pillCircle.angle = angleNum;  
                angleNum += plusAngle;
            }
        }
    }

    public void Upgrade(float b) {
        foreach (Transform child in transform) {
            child.GetComponent<PillCircle>().Upgrade(b);
        }
    }

}
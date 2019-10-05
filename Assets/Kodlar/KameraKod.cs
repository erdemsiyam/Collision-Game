using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KameraKod : MonoBehaviour {

    Rigidbody2D karakterfizik;
    Vector3 aradakiMesafe;
	void Start () {
        karakterfizik = GameObject.FindGameObjectWithTag("karakter").GetComponent<Rigidbody2D>();
        aradakiMesafe = this.transform.position - karakterfizik.transform.position; 

    }

    void Update () {
        this.transform.position = karakterfizik.transform.position + aradakiMesafe; 
    }
}

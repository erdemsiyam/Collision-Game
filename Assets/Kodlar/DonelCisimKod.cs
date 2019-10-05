using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonelCisimKod : MonoBehaviour {

    GameObject buCisim;
    GameObject gezegen;
    Rigidbody2D fizik;
    public float acisalHiz;
    public float buCisimX, buCisimY;
    public float gezegenX, gezegenY;
    public int gezegenBoyut;
    Game gameInstance;
	void Start () {
        while (gameInstance == null)
        {
            gameInstance = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Game>();
        }
        buCisimX = (buCisimX == 0) ? Random.Range(-gameInstance.donelCisimXSinir, gameInstance.donelCisimXSinir) : 0;
        buCisimY = (buCisimY == 0) ? Random.Range(-gameInstance.donelCisimYSinir, gameInstance.donelCisimYSinir) : 0;
        gezegenX = (gezegenX == 0) ? Random.Range(-3, 3) : 0;
        gezegenY = (gezegenY == 0) ? Random.Range(-3, 3) : 0;
        
        float oran = 0.073f; // yeni sprite renderer ile boyut düştü o yüzden eski boyutla şimdiki boyutun oranını aldık 0.285 / ?5
        gezegenBoyut =  Random.Range(3+(Game.staticLevel/5), 7+(Game.staticLevel/3)); // Kaçılan daire boyu, eskiden (3,7), şimdi levele bağlı oldu

        buCisim = gameObject;
        buCisim.transform.position = new Vector3(buCisimX, buCisimY,0);

        gezegen = buCisim.transform.GetChild(0).gameObject;
        gezegen.transform.localPosition = new Vector3(gezegenX, gezegenY, 0);
        gezegen.transform.localScale = new Vector3(gezegenBoyut, gezegenBoyut, 0)*oran;
 


        acisalHiz = (acisalHiz == 0) ? Random.Range(gameInstance.donelHizEnAz,gameInstance.donelHizEnFazla) : 0;
        fizik = buCisim.GetComponent<Rigidbody2D>();
        fizik.angularVelocity = acisalHiz; //Random rotasyon (dönme şekli) verir. ve o sabitlikte dönmeye devam eder.
        


    }

    // Update is called once per frame
    void Update () {
        fizik.angularVelocity = acisalHiz; //Random rotasyon (dönme şekli) verir. ve o sabitlikte dönmeye devam eder.
    }

}

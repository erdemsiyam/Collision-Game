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
        gezegenBoyut =  Random.Range(3, 7);

        buCisim = gameObject;
        buCisim.transform.position = new Vector3(buCisimX, buCisimY,0);

        gezegen = buCisim.transform.GetChild(0).gameObject;
        gezegen.transform.localPosition = new Vector3(gezegenX, gezegenY, 0);
        gezegen.transform.localScale = new Vector3(gezegenBoyut, gezegenBoyut, 0);
        CircleCollider2D gezegenCollider = buCisim.GetComponent<CircleCollider2D>();
        gezegenCollider.offset = new Vector3(gezegenX, gezegenY, 0);
        float colliderRadius = 0;
        switch (gezegenBoyut)
        {
            case 1:
                colliderRadius = 0.085f;
                break;
            case 2:
                colliderRadius = 0.17f;
                break;
            case 3:
                colliderRadius = 0.26f;
                break;
            case 4:
                colliderRadius = 0.34f;
                break;
            case 5:
                colliderRadius = 0.43f;
                break;
            case 6:
                colliderRadius = 0.52f;
                break;
            case 7:
                colliderRadius = 0.60f;
                break;
        }
        gezegenCollider.radius = colliderRadius;


        acisalHiz = (acisalHiz == 0) ? Random.Range(gameInstance.donelHizEnAz,gameInstance.donelHizEnFazla) : 0;
        fizik = buCisim.GetComponent<Rigidbody2D>();
        fizik.angularVelocity = acisalHiz; //Random rotasyon (dönme şekli) verir. ve o sabitlikte dönmeye devam eder.
        


    }

    // Update is called once per frame
    void Update () {
        fizik.angularVelocity = acisalHiz; //Random rotasyon (dönme şekli) verir. ve o sabitlikte dönmeye devam eder.
    }

}

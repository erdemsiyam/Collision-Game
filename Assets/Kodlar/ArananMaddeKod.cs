using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArananMaddeKod : MonoBehaviour { // ARANAN MADDE

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
        gezegenBoyut = 4; // arananMaddenin boyutu sabit : 4.

        buCisim = gameObject;
        buCisim.transform.position = new Vector3(buCisimX, buCisimY, 0);

        gezegen = buCisim.transform.GetChild(0).gameObject;
        gezegen.transform.localPosition = new Vector3(gezegenX, gezegenY, 0);
        gezegen.transform.localScale = new Vector3(gezegenBoyut, gezegenBoyut, 0);
        CircleCollider2D gezegenCollider = buCisim.GetComponent<CircleCollider2D>();
        gezegenCollider.offset = new Vector3(gezegenX, gezegenY, 0);
        gezegenCollider.radius = 0.34f;


        acisalHiz = (acisalHiz == 0) ? Random.Range(gameInstance.donelHizEnAz, gameInstance.donelHizEnFazla) : 0;
        fizik = buCisim.GetComponent<Rigidbody2D>();
        fizik.angularVelocity = acisalHiz; //Random rotasyon (dönme şekli) verir. ve o sabitlikte dönmeye devam eder.

        //arananMaddenin Rengini rastgele yapalım
        gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0F, 1F), Random.Range(0, 1F), Random.Range(0, 1F));
    }

    // Update is called once per frame
    void Update () {
        fizik.angularVelocity = acisalHiz; //Random rotasyon (dönme şekli) verir. ve o sabitlikte dönmeye devam eder.
    }
}

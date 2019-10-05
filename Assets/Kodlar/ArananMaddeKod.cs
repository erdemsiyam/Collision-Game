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
        
        float oran = 0.073f; // yeni sprite renderer ile boyut düştü o yüzden eski boyutla şimdiki boyutun oranını aldık 0.285 / ?4
        gezegenBoyut = 4; // arananMaddenin boyutu sabit : 4.

        buCisim = gameObject;
        buCisim.transform.position = new Vector3(buCisimX, buCisimY, 0);

        gezegen = buCisim.transform.GetChild(0).gameObject;
        gezegen.transform.localPosition = new Vector3(gezegenX, gezegenY, 0);
        gezegen.transform.localScale = new Vector3(gezegenBoyut, gezegenBoyut, 0)*oran;



        acisalHiz = (acisalHiz == 0) ? Random.Range(gameInstance.donelHizEnAz, gameInstance.donelHizEnFazla) : 0;
        fizik = buCisim.GetComponent<Rigidbody2D>();
        fizik.angularVelocity = acisalHiz; //Random rotasyon (dönme şekli) verir. ve o sabitlikte dönmeye devam eder.

        //arananMaddenin Rengini rastgele yapalım
        float randomR,randomG,randomB;
        do{
            randomR = Random.Range(0F, 0.785F); 
            randomG = Random.Range(0F, 0.785F);
            randomB = Random.Range(0F, 0.785F);
        } while(randomR >= 0.785f && randomG >= 0.785f && randomB >= 0.785f || // Neden 0.785 kontrolü, hepsi bundan büyük olursa beyaz gibi gözükür
                ((randomR >= 0.135f && randomR <= 0.14f) && // bu ve altındaki kontroller ise, arka plan rengi ile aynı olmaması için
                 (randomG >= 0.135f && randomG <= 0.14f) &&
                 (randomB >= 0.135f && randomB <= 0.14f) )); 
        gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(randomR,randomG,randomB);
    }

    // Update is called once per frame
    void Update () {
        fizik.angularVelocity = acisalHiz; //Random rotasyon (dönme şekli) verir. ve o sabitlikte dönmeye devam eder.
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GoogleMobileAds.Api;
using System;

public class Game : MonoBehaviour {

    public static int staticLevel = 0;
    public static bool staticLastChance = true; // öldüğünde bulunduğu level'i 1kez daha oynama hakkı
    public static bool flagHighScore = false;
    public static string mesaj;
    public static int toplananTopSayisi = 0;
    public static int kalanZaman = 0;
    public GameObject donelCisim;
    public GameObject arananMadde;
    public GameObject karakter;
    public KarakterKod karakterInstance;
    public Text geriSayimText,bilgilendirmeText,durumText,txtKalanZaman;
    public int level;
    public bool kalanZamanDurdur = false;
    public float karakterXSinir, karakterYSinir;
    public float donelCisimXSinir, donelCisimYSinir;
    public int donelCisimSayisi,arananMaddeSayisi;
    public int donelHizEnAz, donelHizEnFazla;
    public Animator animator;

    public static InterstitialAd interstitial;

	void Start () {

        //oyunun normal süreci için, önceden slow motion açıldıysa kapanır.
        slowMotionKapa();

        //reklamı şimdiden indirmeye başla
        reklamIndir();

        toplananTopSayisi = 0;
        karakter = GameObject.FindGameObjectWithTag("karakter");
        karakterInstance = karakter.GetComponent<KarakterKod>();


        //staticLevel = level = 50; // Unity Kullanımı için
        
        /* Normal kullanım için */
        if (staticLevel == 0)
        {
            level = 1;
            staticLevel = 1;
            staticLastChance = true;
        }
        else
        {
            level = staticLevel;
        }

        kalanZaman = (int)(staticLevel * 3);

        karakterXSinir = 4f + level*1;
        karakterYSinir = 6f + level*1;
        donelCisimXSinir = 2 + level*1;
        donelCisimYSinir = 4 + level*1;
        donelCisimSayisi = 7 + level * 4; // 3
        arananMaddeSayisi = level * 1;
        donelHizEnAz = 50 + level * 1; // 1
        donelHizEnFazla = 80 + level * 2; // 1

        GameObject sagDuvar = GameObject.FindGameObjectWithTag("sagDuvar");
        GameObject solDuvar = GameObject.FindGameObjectWithTag("solDuvar");
        GameObject ustDuvar = GameObject.FindGameObjectWithTag("ustDuvar");
        GameObject altDuvar = GameObject.FindGameObjectWithTag("altDuvar");

        sagDuvar.transform.localPosition = new Vector3(karakterXSinir + 16.5f, 0,1); //duvarı karakterin sınırı kadar iteleyip, kendi yarısının x uzunluğunu da ekledik sol kısmı tam sınıra denk gelmiş oldu
        solDuvar.transform.localPosition = new Vector3(-karakterXSinir - 16.5f, 0,1); //duvarı karakterin sınırı kadar iteleyip, kendi yarısının x uzunluğunu da ekledik sol kısmı tam sınıra denk gelmiş oldu
        ustDuvar.transform.localPosition = new Vector3(0, karakterYSinir + 16.5f, 1); //duvarı karakterin sınırı kadar iteleyip, kendi yarısının x uzunluğunu da ekledik sol kısmı tam sınıra denk gelmiş oldu
        altDuvar.transform.localPosition = new Vector3(0, -karakterYSinir - 16.5f, 1); //duvarı karakterin sınırı kadar iteleyip, kendi yarısının x uzunluğunu da ekledik sol kısmı tam sınıra denk gelmiş oldu
        
        sagDuvar.transform.localScale = new Vector3(200, (karakterYSinir*2+200)*3); // her levele uyumlu duvara boyut verdik
        solDuvar.transform.localScale = new Vector3(200, (karakterYSinir*2+200)*3); // her levele uyumlu duvara boyut verdik
        ustDuvar.transform.localScale = new Vector3((karakterXSinir*2+200)*3, 200); // her levele uyumlu duvara boyut verdik
        altDuvar.transform.localScale = new Vector3((karakterXSinir*2+200)*3, 200); // her levele uyumlu duvara boyut verdik


        for (int i = 0; i <donelCisimSayisi; i++)
        {
            Instantiate(donelCisim, gameObject.transform.position, gameObject.transform.rotation);
        }
        for (int i = 0; i < arananMaddeSayisi; i++)
        {
            Instantiate(arananMadde, gameObject.transform.position, gameObject.transform.rotation);
        }

        StartCoroutine(oyunBaslat());
    }

    IEnumerator kalanZamanBaslat()
    {
        yield return new WaitForSeconds(1); // Oyunu kazandığını görmesi için. 2 saniye animasyona ayırdık

        if(kalanZamanDurdur)
            yield break;

        if(kalanZaman == 0)
        {
            geriSayimText.text = "Time Up";
            gameOver();
        }
        else
        {
            txtKalanZaman.text = --kalanZaman + "";
            StartCoroutine(kalanZamanBaslat());
        }
        yield break;
    }



    // Update is called once per frame
    void Update () {
        GameObject[] arananlar = GameObject.FindGameObjectsWithTag("arananMadde");
        durumText.text = "Lv " + level + "\n" + (level-arananlar.Length) + " / " + level;

    }

    IEnumerator oyunBaslat()
    {
        kalanZamanDurdur = false;
        txtKalanZaman.text = kalanZaman + "";
        yield return new WaitForSeconds(0.25f);
        if (staticLevel == 1 && staticLastChance) // eğer level1 ve ikinic şansı varsa, oyunun başında demektir. Aşağıdaki bilgilendirme yapılır
            bilgilendirmeText.text = "Collect colored circles.\nYou have "+kalanZaman+" seconds.";
        else if (!staticLastChance) // son şansı false ise bitmiş ise, uyarırız.
            bilgilendirmeText.text = "LAST CHANCE !";
        geriSayimText.text = "Level "+level;
        yield return new WaitForSeconds(1f);
        geriSayimText.text = "3";
        yield return new WaitForSeconds(0.8f);
        geriSayimText.text = "2";
        yield return new WaitForSeconds(0.8f);
        geriSayimText.text = "1";
        yield return new WaitForSeconds(0.8f);
        geriSayimText.text = "";
        karakterInstance.oyunBasladiMi = true;

        yield return new WaitForSeconds(1.5f);
        bilgilendirmeText.text = "";
        StartCoroutine(kalanZamanBaslat());

    }

    public void gameOver()
    {
        kalanZamanDurdur = true;
        // slow motion yapıyoruz
        slowMotionAc();

        animator.SetTrigger("yenildiTrigger");
        karakter.GetComponent<CircleCollider2D>().enabled = false;
        karakter.GetComponent<KarakterKod>().enabled = false;
        karakter.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        if (!staticLastChance ) // oynama hakkı kalmamışsa
        {
            
            mesaj = level + " " + toplananTopSayisi.ToString(); // level ve toplanan top bilgisini verdik
            staticLevel = 0; // level'i sıfırla
            staticLastChance = true; // hakkı sıfırlanır
            StartCoroutine(oyunSonuMenuGec());//OyunSonu Menüsüne gönderilir
            return;
        }
        else // oyun oynama hakkı varsa
        {
            staticLastChance = false; // o şansı kullanılır false yapılıp , leveli sıfırlanmaz
        }
        StartCoroutine(levelTekrarla());

        // LEVEL 1 AZALSIN ? yada direkt level 1 gitsin

        /*
        karakter.GetComponent<KarakterKod>().enabled = false;
        GameObject[] donenler = GameObject.FindGameObjectsWithTag("donelCisimTag");
        foreach (GameObject item in donenler)
        {
            item.GetComponent<DonelCisimKod>().enabled = false;
        }
        GameObject[] arananlar = GameObject.FindGameObjectsWithTag("arananMadde");
        foreach (GameObject item in arananlar)
        {
            item.GetComponent<ArananMaddeKod>().enabled = false;
        }
        */
    }
    public void oyunGecildi()
    {        
        kalanZamanDurdur = true;
        // slow motion yapıyoruz
        slowMotionAc();

        animator.SetTrigger("gectiTrigger");
        karakter.GetComponent<CircleCollider2D>().enabled = false;
        karakter.GetComponent<KarakterKod>().enabled = false;
        karakter.GetComponent<Rigidbody2D>().velocity = Vector2.zero; // hızını sıfırladık

        int newLevel = ++staticLevel; //level'i bir artırdık
        saveLevelAsHighIfItIs(newLevel);

        //Son şans hakkını yenile.
        staticLastChance = true;

        //Eğer son level 50'ye geldiyse oyunu bitir.
        if(staticLevel >= 50 && toplananTopSayisi == 50)
        {
            staticLevel = 50;
            setHighLevel(50);
            setHighScore(50);
            mesaj = level + " " + toplananTopSayisi.ToString(); // level ve toplanan top bilgisini verdik
            StartCoroutine(oyunSonuMenuGec());
        }
        else
            StartCoroutine(yeniLeveleGec()); //oyunu yeniden başlat, leveli 1  yükselterek
    }

    IEnumerator yeniLeveleGec()
    {
        yield return new WaitForSeconds(0.5f); // Oyunu kazandığını görmesi için. 2 saniye animasyona ayırdık
        SceneManager.LoadScene("SampleScene");//sahneyi tekrar yükledik
    }
    IEnumerator levelTekrarla()
    {
        yield return new WaitForSeconds(0.5f); // Oyunu kazandığını görmesi için. 2 saniye animasyona ayırdık
        SceneManager.LoadScene("SampleScene");//sahneyi tekrar yükledik
    }
    IEnumerator oyunSonuMenuGec()
    {
        yield return new WaitForSeconds(0.5f); // Oyunu kazandığını görmesi için. 2 saniye animasyona ayırdık
        slowMotionKapa();
        reklamGoster();
        SceneManager.LoadScene("OyunSonu");//sahneyi tekrar yükledik
    }

    public void slowMotionAc(){
        Time.timeScale = 0.2f; 
        Time.fixedDeltaTime =Time.timeScale* 0.02f;
    }
    public void slowMotionKapa(){
        Time.timeScale = 1f; 
        Time.fixedDeltaTime =0.02f;
    }

    private static void reklamIndir()
    {
        string adUnitId;
        #if UNITY_ANDROID
            adUnitId = "ca-app-pub-3940256099942544/1033173712";
        #elif UNITY_IPHONE
            adUnitId = "ca-app-pub-3940256099942544/1033173712";
        #else
            adUnitId = "ca-app-pub-3940256099942544/1033173712";
        #endif

        // Initialize an InterstitialAd.
        interstitial = new InterstitialAd(adUnitId);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder()
            .AddTestDevice("2077ef9a63d2b398840261c8221a0c9b")
            .Build();
        // AdRequest request = new AdRequest.Builder().Build();

        // Load the interstitial with the request.
        interstitial.LoadAd(request);
    }
    private static void reklamGoster()
    {
        if (interstitial.IsLoaded()) {
            interstitial.Show();
        }
    }

    private void saveLevelAsHighIfItIs(int newLevel)
    {
        if(newLevel > getSavedHighLevel())
        {
            flagHighScore = true;
            setHighLevel(newLevel);
            setHighScore(0);
        }
    }
    int getSavedHighLevel()
    {
        return PlayerPrefs.GetInt("highlevel", 0);
    }

    int getSavedHighScore()
    {
        return PlayerPrefs.GetInt("highscore", 0);
    }
    void setHighLevel(int highLevel)
    {
        PlayerPrefs.SetInt("highlevel", highLevel);
    }
    void setHighScore(int highScore)
    {
        PlayerPrefs.SetInt("highscore", highScore);
    }

}
/*
        level 1 :
                    karakter sınır : 4x6 (x,y)      +1
                    donel sınır : 3x5               +1
                    dönel sayısı : 10               +3
                    aranan madde : 1                +1
                    hız : 50x 80                    +1

     
     
     
     
     */

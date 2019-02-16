using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour {

    public static int staticLevel = 0;
    public static bool staticLastChance = true; // öldüğünde bulunduğu level'i 1kez daha oynama hakkı
    public static string mesaj;
    public static int toplananTopSayisi = 0;
    public GameObject donelCisim;
    public GameObject arananMadde;
    public GameObject karakter;
    public KarakterKod karakterInstance;
    public Text geriSayimText,bilgilendirmeText,durumText;
    public int level;
    public float karakterXSinir, karakterYSinir;
    public float donelCisimXSinir, donelCisimYSinir;
    public int donelCisimSayisi,arananMaddeSayisi;
    public int donelHizEnAz, donelHizEnFazla;
    public Animator animator;
	void Start () {
        toplananTopSayisi = 0;
        karakter = GameObject.FindGameObjectWithTag("karakter");
        karakterInstance = karakter.GetComponent<KarakterKod>();

        /*
        level = (level < 1 || level >100) ? 1 : level; // Unity Kullanımı için
        staticLevel = level; // Unity Kullanımı için
        */
        /* Normal kullanım için  */
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



        karakterXSinir = 4 + level*1;
        karakterYSinir = 6 + level*1;
        donelCisimXSinir = 2 + level*1;
        donelCisimYSinir = 4 + level*1;
        donelCisimSayisi = 7 + level * 3;
        arananMaddeSayisi = level * 1;
        donelHizEnAz = 49 + level * 1;
        donelHizEnFazla = 79 + level * 1;

        GameObject.FindGameObjectWithTag("sagDuvar").transform.localPosition = new Vector3(karakterXSinir + 14.5f, 0,1); //duvarı karakterin sınırı kadar iteleyip, kendi yarısının x uzunluğunu da ekledik sol kısmı tam sınıra denk gelmiş oldu
        GameObject.FindGameObjectWithTag("solDuvar").transform.localPosition = new Vector3(-karakterXSinir - 14.5f, 0,1); //duvarı karakterin sınırı kadar iteleyip, kendi yarısının x uzunluğunu da ekledik sol kısmı tam sınıra denk gelmiş oldu
        GameObject.FindGameObjectWithTag("ustDuvar").transform.localPosition = new Vector3(0, karakterYSinir + 14.5f, 1); //duvarı karakterin sınırı kadar iteleyip, kendi yarısının x uzunluğunu da ekledik sol kısmı tam sınıra denk gelmiş oldu
        GameObject.FindGameObjectWithTag("altDuvar").transform.localPosition = new Vector3(0, -karakterYSinir - 14.5f, 1); //duvarı karakterin sınırı kadar iteleyip, kendi yarısının x uzunluğunu da ekledik sol kısmı tam sınıra denk gelmiş oldu

        for (int i = 0; i <donelCisimSayisi; i++)
        {
            Instantiate(donelCisim, gameObject.transform.position, gameObject.transform.rotation);
        }
        for (int i = 0; i < level; i++)
        {
            Instantiate(arananMadde, gameObject.transform.position, gameObject.transform.rotation);
        }

        StartCoroutine(oyunBaslat());
    }
	
	// Update is called once per frame
	void Update () {
        GameObject[] arananlar = GameObject.FindGameObjectsWithTag("arananMadde");
        durumText.text = "Lv " + level + "\n" + (level-arananlar.Length) + "/" + level;
    }

    IEnumerator oyunBaslat()
    {
        yield return new WaitForSeconds(0.25f);
        if (staticLevel == 1 && staticLastChance) // eğer level1 ve ikinic şansı varsa, oyunun başında demektir. Aşağıdaki bilgilendirme yapılır
            bilgilendirmeText.text = "Do Not Touch The White Circles\n Catch The Which Circle Diffrent Color.";
        else if (!staticLastChance) // son şansı false ise bitmiş ise, uyarırız.
            bilgilendirmeText.text = "LAST CHANCE !!!";
        geriSayimText.text = "Level "+level;
        yield return new WaitForSeconds(1.5f);
        geriSayimText.text = "3";
        yield return new WaitForSeconds(0.8f);
        geriSayimText.text = "2";
        yield return new WaitForSeconds(0.8f);
        geriSayimText.text = "1";
        yield return new WaitForSeconds(0.8f);
        geriSayimText.text = "0";
        yield return new WaitForSeconds(0.6f);
        geriSayimText.text = "";
        karakterInstance.oyunBasladiMi = true;


        yield return new WaitForSeconds(1.5f);
        bilgilendirmeText.text = "";

    }

    public void gameOver()
    {
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
        StartCoroutine(levelBireDon());

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
        animator.SetTrigger("gectiTrigger");
        karakter.GetComponent<CircleCollider2D>().enabled = false;
        karakter.GetComponent<KarakterKod>().enabled = false;
        karakter.GetComponent<Rigidbody2D>().velocity = Vector2.zero; // hızını sıfırladık
        //oyunu yeniden başlat, leveli 1  yükselterek
        staticLevel++; //level'i bir artırdık
        staticLastChance = true;
        StartCoroutine(yeniLeveleGec());
    }
    IEnumerator yeniLeveleGec()
    {
        yield return new WaitForSeconds(3); // Oyunu kazandığını görmesi için. 2 saniye animasyona ayırdık
        SceneManager.LoadScene("SampleScene");//sahneyi tekrar yükledik
    }
    IEnumerator levelBireDon()
    {
        yield return new WaitForSeconds(3); // Oyunu kazandığını görmesi için. 2 saniye animasyona ayırdık
        SceneManager.LoadScene("SampleScene");//sahneyi tekrar yükledik
    }
    IEnumerator oyunSonuMenuGec()
    {
        yield return new WaitForSeconds(3); // Oyunu kazandığını görmesi için. 2 saniye animasyona ayırdık
        SceneManager.LoadScene("OyunSonu");//sahneyi tekrar yükledik
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

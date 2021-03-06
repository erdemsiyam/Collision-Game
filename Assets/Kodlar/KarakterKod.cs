﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarakterKod : MonoBehaviour {

    Rigidbody2D fizik;
    Game gameInstance;
    public bool oyunBasladiMi = false;
    public int hiz;
    public float karakterXSinir;
    public float karakterYSinir;
    public bl_Joystick joystick;
    void Start () {
        while (gameInstance == null)
        {
            gameInstance = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Game>();
        }
        karakterXSinir = gameInstance.karakterXSinir;
        karakterYSinir = gameInstance.karakterYSinir;
        fizik = gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        
        float yatay = joystick.Horizontal;//Input.GetAxis("Horizontal"); // sadece 1 0 -1 
        float dikey = joystick.Vertical;//Input.GetAxis("Vertical");

        yatay /= 5f;
        dikey /= 5f;

        Vector3 vec = new Vector3(yatay,dikey,0);
        fizik.velocity = vec * hiz;

        fizik.position = new Vector3( // KARAKTERİN YERİNİ SINIRLAMA
            Mathf.Clamp(fizik.position.x, -karakterXSinir, karakterXSinir), // x'i alırız, bu değerler arasından çıkamaz deriz, ve çıkmaz: yatayda
            Mathf.Clamp(fizik.position.y, -karakterYSinir, karakterYSinir),// y ' ekseni de durduk yere o euler fonksiyonu olsa gerek, saçma saçma aşaığı yukarı gidiyor, onu engelleriz.
            Mathf.Clamp(0, 0, 0) 
        );

    }

    void OnTriggerEnter2D(Collider2D carpanOge)
    {
        if (carpanOge.gameObject.tag == "donelCisimTag" && oyunBasladiMi)
        {//beyazlara çarparsak, o çarpanı çarptığı yerde sabitleyip, gameOver metodunu çağıralım
            carpanOge.gameObject.transform.parent.GetComponent<DonelCisimKod>().enabled = false; // onu yönlendiren kodu devre dışı bıraktık, parentine ulaştık ordan (gameObject.transform.parent.)
            carpanOge.gameObject.transform.parent.GetComponent<Rigidbody2D>().angularVelocity = 0; // hızını sıfırladık, parentine ulaştık ordan (gameObject.transform.parent.)
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Game>().gameOver();
        }
        else if (carpanOge.gameObject.tag == "arananMadde" && oyunBasladiMi)
        {
            int numOfCollect = ++Game.toplananTopSayisi;
            saveScoreAsHighIfItIs(numOfCollect);
            gameObject.GetComponent<SpriteRenderer>().color = carpanOge.GetComponent<SpriteRenderer>().color; // alınan nesnenin rengi de karaktere alınır.
            if (GameObject.FindGameObjectsWithTag("arananMadde").Length == 1)
            {//sonuncu maddeyi alıyorsak
                //OYUN BİTTİ
                carpanOge.gameObject.SetActive(false);
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Game>().oyunGecildi();
            }
            else
            { // sonuncu maddeyi almıyorsak : o maddeyi etkisiz kılalım
                carpanOge.gameObject.SetActive(false);
            }
        }

    }

    private void saveScoreAsHighIfItIs(int newScore)
    {
        if(Game.staticLevel >= getSavedHighLevel())
        {
            if( newScore > getSavedHighScore())
            {
                Game.flagHighScore = true;
                setHighScore(newScore);
            }
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
    void setHighScore(int highScore)
    {
        PlayerPrefs.SetInt("highscore", highScore);
    }
}

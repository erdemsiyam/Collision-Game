using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OyunSonuButtonKod : MonoBehaviour {

    public Text bilgilendirmeText;
    void Start()
    {
        string[] mesaj = Game.mesaj.Split(' ');
        string level = mesaj[0];
        string toplananTop = mesaj[1];
        bilgilendirmeText.text = "Level "+level+"\n"+toplananTop+"/"+level;
    }
    public void oyunaGir()
    {
        SceneManager.LoadScene("SampleScene");
        Debug.Log("ads");
    }

    public void oyunMenu()
    {
        SceneManager.LoadScene("OyunMenu");
    }
}

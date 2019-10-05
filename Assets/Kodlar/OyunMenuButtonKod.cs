using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OyunMenuButtonKod : MonoBehaviour {

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) { Application.Quit(); }
    }
    public void oyunaGir()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void cik()
    {
        Application.Quit();
    }
}

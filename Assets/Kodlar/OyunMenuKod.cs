using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OyunMenuKod : MonoBehaviour {

    public Text markaText;
    public Text txtHighScore;
    public GameObject donelCisim;
	void Start () {
        Screen.SetResolution(1080, 1920, true);
        //oyun menü arkasına rastgele renk ve oyun ismi de
        //gameObject.GetComponent<Camera>().backgroundColor = new Color(Random.Range(0F, 1F), Random.Range(0, 1F), Random.Range(0, 1F));
        //markaText.color = new Color(Random.Range(0F, 1F), Random.Range(0, 1F), Random.Range(0, 1F));

        /* High Score Writing. */
        int highlevel = getSavedHighLevel();
        if(highlevel > 0)
        {
            int highScore = getSavedHighScore();
            txtHighScore.text = "High Score\nLv "+highlevel+"\n"+highScore+" / "+highlevel;
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
}

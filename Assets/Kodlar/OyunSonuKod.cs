using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OyunSonuKod : MonoBehaviour {

	public Text txtHighScore;
	public Text txtGameOver;
	void Start () {
		
		string[] mesaj = Game.mesaj.Split(' ');
        int level = int.Parse(mesaj[0]);
        int toplananTop = int.Parse(mesaj[1]);
		if(level == 50 && toplananTop == 50)
		{
			txtGameOver.text = "YOU WON";
			this.gameObject.GetComponent<Camera>().backgroundColor = new Color(0.73f,0.96f,0.52f); // Green background
			txtHighScore.text = "* High Score *";
			Game.flagHighScore = false;
		}
		else if(Game.flagHighScore)
		{
			this.gameObject.GetComponent<Camera>().backgroundColor = new Color(0.96f,0.85f,0.52f); // Orange background
			txtHighScore.text = "* High Score *";
			Game.flagHighScore = false;
		} else {
			this.gameObject.GetComponent<Camera>().backgroundColor = new Color(0.96f,0.52f,0.59f); // RED background
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

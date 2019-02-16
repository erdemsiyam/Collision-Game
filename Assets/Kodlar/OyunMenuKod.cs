using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OyunMenuKod : MonoBehaviour {

    public Text markaText;
    public GameObject donelCisim;
	void Start () {
        Screen.SetResolution(1080, 1920, true);
        gameObject.GetComponent<Camera>().backgroundColor = new Color(Random.Range(0F, 1F), Random.Range(0, 1F), Random.Range(0, 1F)); // oyun menü arkasına rastgele renk
        markaText.color = new Color(Random.Range(0F, 1F), Random.Range(0, 1F), Random.Range(0, 1F));
    }


}

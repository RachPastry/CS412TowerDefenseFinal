using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreManager : MonoBehaviour
{
    public Text highScoreText;


    // Start is called before the first frame update
    void Start()
    {
        highScoreText.text =
            PlayerPrefs.GetString("HSP0") + "                    " + PlayerPrefs.GetInt("HS0").ToString() + "\n" +
            PlayerPrefs.GetString("HSP1") + "                    " + PlayerPrefs.GetInt("HS1").ToString() + "\n" +
            PlayerPrefs.GetString("HSP2") + "                    " + PlayerPrefs.GetInt("HS2").ToString() + "\n" +
            PlayerPrefs.GetString("HSP3") + "                    " + PlayerPrefs.GetInt("HS3").ToString() + "\n" +
            PlayerPrefs.GetString("HSP4") + "                    " + PlayerPrefs.GetInt("HS4").ToString() + "\n";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

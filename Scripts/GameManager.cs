using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject FireTrapPrefab;
    public GameObject CutterTrapPrefab;
    public GameObject BladeTrapPrefab;
    public EnemySpawner myEnemySpawner;
    public bool GameIsOver = false;
    public bool ShouldCheckForAllEnemiesDead = false;


    public GameObject beginWaveButton;
    public Text currencyText;
    public int gold = 200;

    public AudioSource myaudiosource;
    
    private GameObject[] AllButtons;
    private GameObject latestTrap;
    private bool placingTrapNow = false;


    // Start is called before the first frame update
    void Start()
    {

        PlayerPrefs.SetInt("CurrentLevel", SceneManager.GetActiveScene().buildIndex);
        int buildIndex = SceneManager.GetActiveScene().buildIndex;
        print("BuildIndex" + buildIndex + "PlayerPrefs Gold " + PlayerPrefs.GetInt("CurrentGold"));
        gold = gold + PlayerPrefs.GetInt("CurrentGold");
        currencyText.text = "Current Currency : " + gold;
        myEnemySpawner.StartEnemySpawner();
        myEnemySpawner.DoneSpawning = true;


    }

    public void BeginWaveButtonHandler() {
        myEnemySpawner.DoneSpawning = false;
        beginWaveButton.SetActive(false);
        currencyText.text = " ";
        AllButtons = GameObject.FindGameObjectsWithTag("Button");
        foreach (GameObject button in AllButtons) {
            button.SetActive(false);
        }
    }

    public void GenericArrowButtonHandler(int arrowID) {
        if (placingTrapNow) {
            myaudiosource.Play();
            switch (arrowID) {
                case 0: //Up Button
                    latestTrap.transform.Translate(new Vector3(0, 0, 1));
                    break;
                case 1: //Down Button
                    latestTrap.transform.Translate(new Vector3(0, 0, -1));
                    break;
                case 2: //Left Button
                    latestTrap.transform.Translate(new Vector3(-1, 0, 0));
                    break;
                case 3: //Right Button
                    latestTrap.transform.Translate(new Vector3(1, 0, 0));
                    break;
            }
        }
    }

    public void GenericTrapButtonHandler(int TrapID) {
        switch (TrapID) {
            case 0: //BladeTrap
                if (gold >= 50) {
                    gold -= 50;
                    currencyText.text = "Current Currency : " + gold;
                    //Spawn and place trap
                    GameObject myBladeTrap = Instantiate(BladeTrapPrefab);
                    myBladeTrap.transform.position = new Vector3(0,0,0);
                    myBladeTrap.SetActive(true);
                    placingTrapNow = true;
                    latestTrap = myBladeTrap;
                }
                break;
            case 1: //CutterTrap
                if (gold >= 60)
                {
                    gold -= 60;
                    currencyText.text = "Current Currency : " + gold;
                    //Spawn and place trap
                    GameObject myCutterTrap = Instantiate(CutterTrapPrefab);
                    myCutterTrap.transform.position = new Vector3(0, 0, 0);
                    myCutterTrap.SetActive(true);
                    placingTrapNow = true;
                    latestTrap = myCutterTrap;

                }
                break;
            case 2: //FireTrap
                if (gold >= 100)
                {
                    gold -= 100;
                    currencyText.text = "Current Currency : " + gold;
                    //Spawn and place trap
                    GameObject myFireTrap = Instantiate(FireTrapPrefab);
                    myFireTrap.transform.position = new Vector3(0, 0, 0);
                    myFireTrap.SetActive(true);
                    placingTrapNow = true;
                    latestTrap = myFireTrap;
                }
                break;
        }
    }

  

    public void WaveIsDoneSpawning() {
        myEnemySpawner.DoneSpawning = true;
        ShouldCheckForAllEnemiesDead = true;
             
    }

    public void BeatTheWave() {
       
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
 //       print("Beat the wave Current Level" + currentLevel);
        gold += 100;
        currencyText.text = "Current Currency : " + gold;
       
        print("Beat the wave, gold is " + gold);
        PlayerPrefs.SetInt("CurrentGold", gold);
        print("Beat the wave, PlayerPrefsGold " + PlayerPrefs.GetInt("CurrentGold"));
        if (myEnemySpawner.waveIndex == myEnemySpawner.ZombiesPerWave.Length && currentLevel == 0) {
            UnityEngine.SceneManagement.SceneManager.LoadScene("LevelVictory");
        }
        if (myEnemySpawner.waveIndex == myEnemySpawner.ZombiesPerWaveLv2.Length && currentLevel == 1)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("LevelVictory");
        }
        if (myEnemySpawner.waveIndex == myEnemySpawner.ZombiesPerWaveLv3.Length && currentLevel == 2)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("LevelVictory");
        }
        ShouldCheckForAllEnemiesDead = false;
        beginWaveButton.SetActive(true);
        
        foreach (GameObject button in AllButtons)
        {
            button.SetActive(true);
        }
    }

    void EndTheGame() {
        if (GameIsOver)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameOverScene");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameIsOver) {
            EndTheGame();
        }

        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Zombie");
        if (allEnemies.Length == 0 && ShouldCheckForAllEnemiesDead) {
            BeatTheWave();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemySpawner : MonoBehaviour
{
    public GameManager myGameManager;
    public GameObject originalZombie4Prefab;
    public GameObject originalZombie2Prefab;
    public GameObject originalZombie1Prefab;
    public GameObject spawnLocation;
    public GameObject spawnLocation1;
    public bool DoneSpawning = false;
    public int waveIndex = 0;

    public int[] ZombiesPerWave = new int[3]; //First Vector = Wave 1. Number of Zombies. 
    public int[] ZombiesPerWaveLv2 = new int[4];
    public int[] ZombiesPerWaveLv3 = new int[5];

    private int[] CurrentZombiesPerWave;
    private int numberOfZombiesSpawned=0;
    private float SpawnTimer = 0.05f;
    private float WaveGapTimer;

    private int CurrentLevel;


    // Start is called before the first frame update
    public void StartEnemySpawner()
    {
        CurrentLevel = PlayerPrefs.GetInt("CurrentLevel");
        print("Level Is EnemySpawner" + CurrentLevel);
        print("EnemySpawner DoneSpawning" + DoneSpawning);

        ZombiesPerWave[0] = 2;  //First Vector = Wave 1. Number of Zombies. 
        ZombiesPerWave[1] = 4;
        ZombiesPerWave[2] = 5;

        ZombiesPerWaveLv2[0] = 4;
        ZombiesPerWaveLv2[1] = 5;
        ZombiesPerWaveLv2[2] = 8;
        ZombiesPerWaveLv2[3] = 6;

        ZombiesPerWaveLv3[0] = 3;
        ZombiesPerWaveLv3[1] = 5;
        ZombiesPerWaveLv3[2] = 7;
        ZombiesPerWaveLv3[3] = 9;
        ZombiesPerWaveLv3[4] = 7;

        if (CurrentLevel == 0) CurrentZombiesPerWave = ZombiesPerWave;
        if (CurrentLevel == 1) CurrentZombiesPerWave = ZombiesPerWaveLv2;
        if (CurrentLevel == 2) CurrentZombiesPerWave = ZombiesPerWaveLv3;
    }



    // Update is called once per frame
    void Update()
    {
       
        if (!DoneSpawning)
        {
         
            if (numberOfZombiesSpawned < CurrentZombiesPerWave[waveIndex])
            {
               
                SpawnTimer -= Time.deltaTime;
                if (SpawnTimer < 0)
                {
                  if (CurrentLevel == 0)
                  {

                    SpawnTimer = 0.5f;
                    GameObject myZombie = Instantiate(originalZombie4Prefab);
                    myZombie.SetActive(true);
                    myZombie.transform.position = spawnLocation.transform.position;
                    numberOfZombiesSpawned++;
                  }

                     if (CurrentLevel == 1)
                    {
                        print("Double Spawning Zombies");
                        SpawnTimer = 0.5f;
                         GameObject myZombie = Instantiate(originalZombie4Prefab);
                         myZombie.SetActive(true);
                         myZombie.transform.position = spawnLocation.transform.position;
                         numberOfZombiesSpawned++; 

                        GameObject myZombie2 = Instantiate(originalZombie2Prefab);
                        myZombie2.SetActive(true);
                        myZombie2.transform.position = spawnLocation1.transform.position;
                        numberOfZombiesSpawned++;
                      }

                    if (CurrentLevel == 2)
                    {
                        print("Double Spawning Zombies");
                        SpawnTimer = 0.5f;
                        GameObject myZombie = Instantiate(originalZombie4Prefab);
                        myZombie.SetActive(true);
                        myZombie.transform.position = spawnLocation.transform.position;
                        numberOfZombiesSpawned++;

                        GameObject myZombie2 = Instantiate(originalZombie2Prefab);
                        myZombie2.SetActive(true);
                        myZombie2.transform.position = spawnLocation1.transform.position;
                        numberOfZombiesSpawned++;

                        GameObject myZombie3 = Instantiate(originalZombie1Prefab);
                        myZombie3.SetActive(true);
                        myZombie3.transform.position = spawnLocation.transform.position;
                        numberOfZombiesSpawned++;

                    }
                }

            }

            else
            {
                print("EnemySpawner WaveisDoneSpawning");
                myGameManager.WaveIsDoneSpawning();


                waveIndex++;
                if (waveIndex == CurrentZombiesPerWave.Length)
                {
                    // waveIndex--;
                    DoneSpawning = true;

                }
                numberOfZombiesSpawned = 0;

            }
        }
    }
    
}


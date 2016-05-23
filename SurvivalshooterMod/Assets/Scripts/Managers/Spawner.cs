using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    //----------------------------------
    // Different Spawn types
    //----------------------------------
    public enum SpawnTypes
    {
        Wave,
        TimedWave
    }

    //To load prefabs of enemies
    public GameObject Enemy;

    //----------------------------------
    // Enemies and how many have been created and how many are to be created
    //----------------------------------
    public int totalEnemy = 10;
    private int numEnemy = 0;
    private int spawnedEnemy = 0;
    public bool increaseonce = false;
    //----------------------------------
    // End of Enemy Settings
    //----------------------------------


    // The ID of the spawner
    private int SpawnID;

    //----------------------------------
    // Different Spawn states and ways of doing them
    //----------------------------------
    private bool waveSpawn = false;
    public bool Spawn = true;
    public SpawnTypes spawnType = SpawnTypes.TimedWave;
    // timed wave controls
    public float waveTimer = 30.0f;
    private float timeTillWave = 0.0f;
    //Wave controls
    public int totalWaves = 5;
    private int numWaves = 0;
    public int wavenumber = 0;
    public float wavecounter = 30.0f;
    //----------------------------------
    // End of Different Spawn states and ways of doing them
    //----------------------------------
    void Start()
    {
        // sets a random number for the id of the spawner
        SpawnID = Random.Range(1, 500);
    }
 
    void Update()
    {
        if (Spawn)
        {
            //spawns enemies in waves, so once all are dead, spawns more
            if (spawnType == SpawnTypes.Wave)
            {
                if (numWaves < totalWaves + 1)
                {
                    if (waveSpawn)
                    {
                        //spawns an enemy
                        spawnEnemy();
                    }
                    if (numEnemy == 0)
                    {
                        // enables the wave spawner
                        waveSpawn = true;
                        //increase the number of waves
                        numWaves++;
                    }
                    if (numEnemy == totalEnemy)
                    {
                        // disables the wave spawner
                        waveSpawn = false;
                    }
                }
            }
            // Spawns enemies in waves but based on time.
            else if (spawnType == SpawnTypes.TimedWave)
            {
                // checks if the number of waves is bigger than the total waves
                if (numWaves <= totalWaves)
                {
                    //Update the wave number & counter every time we have a new wave
                    WaveText.WaveNumber = wavenumber;
                    WaveText.WaveCounter = wavecounter;
                    // Increases the timer to allow the timed waves to work
                    timeTillWave += Time.deltaTime;

                    //Update wave counter to find out time till next wave
                    wavecounter -= Time.deltaTime;

                    if (waveSpawn)
                    {
                        //spawns an enemy
                        spawnEnemy();
                    }
                    // checks if the time is equal to the time required for a new wave
                    if (timeTillWave >= waveTimer)
                    {
                        // enables the wave spawner
                        waveSpawn = true;
                        // sets the time back to zero
                        timeTillWave = 0.0f;

                        //sets time till next wave back to 30
                        wavecounter = 30.0f;

                        // increases the number of waves
                        numWaves++;
                        // A hack to get it to spawn the same number of enemies regardless of how many have been killed
                        numEnemy = 0;

                        if (!increaseonce)
                        {
                            totalEnemy += 1;
                        }
                        wavenumber++;
                    }
                    if (numEnemy >= totalEnemy)
                    {
                        // diables the wave spawner
                        waveSpawn = false;
                    }
                }
                else
                {
                    Spawn = false;
                }
            }
        }
    }
    // spawns an enemy
    private void spawnEnemy()
    {
        Instantiate(Enemy, gameObject.transform.position, Quaternion.identity);
        // Increase the total number of enemies spawned and the number of spawned enemies
        numEnemy++;
        spawnedEnemy++;
    }
    // Call this function from the enemy when it "dies" to remove an enemy count
    public void killEnemy(int sID)
    {
        // if the enemy's spawnId is equal to this spawnersID then remove an enemy count
        if (SpawnID == sID)
        {
            numEnemy--;
        }
    }
    //enable the spawner based on spawnerID
    public void enableSpawner(int sID)
    {
        if (SpawnID == sID)
        {
            Spawn = true;
        }
    }
    //disable the spawner based on spawnerID
    public void disableSpawner(int sID)
    {
        if (SpawnID == sID)
        {
            Spawn = false;
        }
    }
    // returns the Time Till the Next Wave, for a interface, ect.
    public float TimeTillWave
    {
        get
        {
            return timeTillWave;
        }
    }
    // Enable the spawner, useful for trigger events because you don't know the spawner's ID.
    public void enableTrigger()
    {
        Spawn = true;
    }
}
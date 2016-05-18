using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;       // Reference to the player's heatlh.
    public EnemyHealth enemyHealth;         // Reference to enemey's health
    public GameObject enemy;                // The enemy prefab to be spawned.
    public float spawnTime = 3f;            // How long between each spawn.
    public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.
    public int MaxEnemies = 10;             // Maximum number of enemies allowed to spawn at a time

    int CurrentEnemies = 0;
    bool KeepSpawning;

    void Start()
    {
        // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
        InvokeRepeating("Spawn", spawnTime, spawnTime);
        KeepSpawning = true;
    }

    void Update()
    {
        //set keep spawning to false if enemies more than the number allowed
        if (CurrentEnemies >= MaxEnemies)
        {
            // set keepspawning to false to avoid too many enemies to spawn at once
            KeepSpawning = false;
        }

        //reduce enemy count if all enemies are dead
        if (enemyHealth.currentHealth <= 0f)
        {
            CurrentEnemies--;
        }

        // set keepspawning to true if all enemies are dead
        if (CurrentEnemies <= 0)
        {
            KeepSpawning = true;
        }

       
    }

    void Spawn()
    {
        // If the player has no health left...
        if (playerHealth.currentHealth <= 0f)
        {
            // ... exit the function.
            return;
        }

        //if keepspawning is true then spawn enemies
        if (KeepSpawning)
        {
            // Find a random index between zero and one less than the number of spawn points.
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);

            // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
            Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
            //increase count for current enemies
            CurrentEnemies++;
        }
    }
}
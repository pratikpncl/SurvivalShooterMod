using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public EnemyHealth enemyHealth;
    public GameObject enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;

    void Start ()
    {
        //Spawn for specified time
        InvokeRepeating ("Spawn", spawnTime, spawnTime);
    }




    void SpawnEnemy ()
    {
        //Don't spawn if health is 0
        if(playerHealth.currentHealth <= 0f)
        {
            return;
        }

        //Determine enemy spawn point and spawn them
        int spawnPointIndex = Random.Range (0, spawnPoints.Length);
        Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }

    
}

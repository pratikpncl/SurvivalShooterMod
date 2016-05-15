using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;


    void Start ()
    {
        //Spawn for specified time
        InvokeRepeating ("Spawn", spawnTime, spawnTime);
    }


    void Spawn ()
    {
        //Don't spawn if health is 0
        if(playerHealth.currentHealth <= 0f)
        {
            return;
        }

        int spawnPointIndex = Random.Range (0, spawnPoints.Length);
        //Determine enemy spawn point
        Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }
}

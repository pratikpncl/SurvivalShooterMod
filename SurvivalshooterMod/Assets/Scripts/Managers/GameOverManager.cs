using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public PlayerHealth playerHealth;


    Animator anim;


    void Awake()
    {
        //Get Component for animation
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        //if health is 0 then play death animation
        if (playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger("GameOver");
        }
    }
}

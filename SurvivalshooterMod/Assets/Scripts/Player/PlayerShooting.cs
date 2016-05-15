using UnityEngine;
using CnControls;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;
    public float timeBetweenBullets = 0.15f;
    public float range = 100f;
    public bool AndroidInput = true;

    float timer;
    Ray shootRay;
    RaycastHit shootHit;
    int shootableMask;
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    AudioSource gunAudio;
    Light gunLight;
    float effectsDisplayTime = 0.2f;


    void Awake ()
    {
        //Get components for shooting, particles, audio and collision
        shootableMask = LayerMask.GetMask ("Shootable");
        gunParticles = GetComponent<ParticleSystem> ();
        gunLine = GetComponent <LineRenderer> ();
        gunAudio = GetComponent<AudioSource> ();
        gunLight = GetComponent<Light> ();
    }


    void Update ()
    {
        //Update every second
        timer += Time.deltaTime;
        
        if (!AndroidInput)
        {
            //use keyboard if android input is false
            if(Input.GetButton ("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0)
            {
                Shoot ();
            }
        }
        if (AndroidInput)
        {
            //use joystick if android input is true
            if ((CnInputManager.GetAxis("HorizontalDirection") != 0 || CnInputManager.GetAxis("VerticalDirection") != 0) && timer >= timeBetweenBullets)
            {
                // ... shoot the gun
                Shoot();
            }
        }

        if(timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects ();
        }
    }


    public void DisableEffects ()
    {
        //disable effects if you let go of fire key
        gunLine.enabled = false;
        gunLight.enabled = false;
    }


    void Shoot ()
    {
        //Set timer back to 0
        timer = 0f;
        //play shoot audio
        gunAudio.Play ();
        //enable gun light
        gunLight.enabled = true;
        //Stop and play gun particles
        gunParticles.Stop ();
        gunParticles.Play ();

        gunLine.enabled = true;
        gunLine.SetPosition (0, transform.position);
        //shooting mechanism
        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        if(Physics.Raycast (shootRay, out shootHit, range, shootableMask))
        {
            //check enemy for health
            EnemyHealth enemyHealth = shootHit.collider.GetComponent <EnemyHealth> ();
            if(enemyHealth != null)
            {
                //If it has health then shoot
                enemyHealth.TakeDamage (damagePerShot, shootHit.point);
            }
            gunLine.SetPosition (1, shootHit.point);
        }
        else
        {
            gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
        }
    }
}

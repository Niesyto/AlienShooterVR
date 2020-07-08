using UnityEngine;
using System.Collections;

/** @brief Attack the player. */
public class EnemyAttack : MonoBehaviour
{
    /** The time in seconds between each attack. */
    public float timeBetweenAttacks = 1.0f;     
    /** The amount of health taken away per attack. */
    public int attackDamage = 10;
    /** Reference to attacking sound **/
    public AudioClip enemyAttackSound;
    /** Reference to the audio source. */
    AudioSource zombieAudioSource;
    /** Reference to the animator component. */
    Animator anim;                              
    /** Reference to the player GameObject. */
    GameObject player;                          
    /** Reference to the player's health. */
    PlayerHealth playerHealth;                 
    /** Reference to this enemy's health. */
    EnemyHealth enemyHealth;                   
    /** Whether player is within the trigger collider and can be attacked. */
    bool playerInRange;                        
    /** Timer for counting up to the next attack. */
    float timer;                               

    /** @brief Setting up the references. */
    void Awake ()
    {
       
        player = GameObject.FindGameObjectWithTag ("Player");
        playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent <Animator> ();
        zombieAudioSource = GetComponent<AudioSource>();
    }

    /** @brief Check what's entering the collider
    @param other Object interacting with this enemy collider
     */
    void OnTriggerEnter (Collider other)
    { 
        // If the entering collider is the player...
        if(other.gameObject.tag == "Player")
        {
            // ... the player is in range.
            playerInRange = true;
        }
    }

    /** @brief Check what's leaving the collider
    @param other Object interacting with this enemy collider
     */
    void OnTriggerExit (Collider other)
    {
        // If the exiting collider is the player...
        if(other.gameObject == player)
        {
            // ... the player is no longer in range.
            playerInRange = false;
        }
    }

    /** @brief Check if attacking is possible */
    void Update ()
    {
        // Add the time since Update was last called to the timer.
        timer += Time.deltaTime;

        // If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
        if(timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
        {
          
            // ... attack.
            Attack ();
            anim.SetTrigger("isAttacking");
        }
        else if(timer >= timeBetweenAttacks)
        {
            anim.ResetTrigger("isAttacking");
        }
    }

    /** @brief Deal damage to the player */
    void Attack ()
    {
        // Reset the timer.
        timer = 0f;

        // Change the pitch of the sound
        zombieAudioSource.pitch = Random.Range(zombieAudioSource.pitch * 0.9f, zombieAudioSource.pitch * 1.1f);
        zombieAudioSource.PlayOneShot(enemyAttackSound);

        // If the player has health to lose...
        if (playerHealth.currentHealth > 0)
        {
            // ... damage the player.
            playerHealth.TakeDamage (attackDamage);
        }
    }
} 
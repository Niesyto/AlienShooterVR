using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/** @brief Class responsible for handling player's health, damage taking and dying */
public class PlayerHealth : MonoBehaviour
{
    /** The amount of health the player starts the game with. */
    public int startingHealth = 100;                           
    /** The current health the player has. */
    public int currentHealth;                                  
    /** The audio clip to play when the player dies. */
    public AudioClip deathClip;                                 
                                            
    /** Reference to the AudioSource component. */
    AudioSource playerAudio;                                    
                             
    /** Reference to the shooting component */
    PlayerShooting[] playerShooting;                             
    /** Reference to the zombie spawn manager */
    public SpawnManager spawnManager;                                  
    /** True when the player is dead. */
    bool isDead;                                                
    /** True when the player gets damaged. */
    bool damaged;                                              


    /** @brief Setting up references */
    void Awake ()
    {
        // Setting up the references.
        playerAudio = GetComponent <AudioSource> ();
        playerShooting= GetComponentsInChildren <PlayerShooting> ();

        // Set the initial health of the player.
        currentHealth = startingHealth;
    }


    /** @brief Take some damage
    @param amount Amount of damage taken */
    public void TakeDamage (int amount)
    {
        BloodManager.damaged = true;

        // Reduce the current health by the damage amount.
        currentHealth -= amount;


        // Play the hurt sound effect.
        playerAudio.Play ();

        // If the player has lost all it's health and the death flag hasn't been set yet...
        if(currentHealth <= 0 && !isDead)
        {
            // ... it should die.
            Death ();
        }
    }

    /** @brief Handling of player's death */
    void Death ()
    {
        // Set the death flag so this function won't be called again.
        isDead = true;

   

        // Set the audiosource to play the death clip and play it (this will stop the hurt sound from playing).
        playerAudio.clip = deathClip;
        playerAudio.Play ();

        // Turn off the movement and shooting scripts.
        foreach(PlayerShooting a in playerShooting)
        {
            a.enabled = false;
        }
        playerAudio.loop = false;

      
        CharacterController controller = GetComponentInParent<CharacterController>();
        controller.enabled = false;


        DeathHUDManager.isDead = true;

        spawnManager.ResetEnemies();
    }        

    
}


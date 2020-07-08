using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** @brief MedKit trigger handling */
public class MedKitTriger : MonoBehaviour
{
    /** Reference to the player */
    Transform player;   
    /** Reference to the player's health. */            
    PlayerHealth playerHealth; 

    /** @brief Set up references */         
    void Start()
    {
         player = GameObject.FindGameObjectWithTag ("Player").transform;
         playerHealth = player.GetComponent <PlayerHealth> ();
    }

   
    /** @brief Heal the player on entering the trigger zone
    @param other Object entering the collider
     */
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Player")
        {
            double healValue=playerHealth.startingHealth *0.2;
            playerHealth.currentHealth +=(int)healValue;
            if(playerHealth.currentHealth>playerHealth.startingHealth)
                playerHealth.currentHealth=playerHealth.startingHealth;
            Destroy(transform.parent.gameObject);
        }
    }
}

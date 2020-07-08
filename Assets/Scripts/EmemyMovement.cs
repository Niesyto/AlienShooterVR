using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** @brief Calculating the movement of an enemy */
public class EmemyMovement : MonoBehaviour
{
    /** Reference to the animator component. */
    Animator anim;                            
    /** Reference to the player's position. */
    Transform player;              
    /** Reference to the player's health. */
    PlayerHealth playerHealth;     
    /** Reference to this enemy's health. */
    EnemyHealth enemyHealth;       
    /** Reference to the nav mesh agent. */
    UnityEngine.AI.NavMeshAgent nav;             


    /** @brief Set up references. */
    void Awake()
    {


 	    anim = GetComponent<Animator>();

        player = GameObject.FindGameObjectWithTag ("Player").transform;
        playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent <EnemyHealth> ();
        nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();
	   
    }
    
    /** @brief Update nav mesh destination. */
    void Update ()
    {
        // If the enemy and the player have health left...
        if(playerHealth.currentHealth > 0 && enemyHealth.currentHealth>=0)
        {
            anim.SetBool("isWalking", true);
            // ... set the destination of the nav mesh agent to the player.
            nav.SetDestination (player.position);
        }
        // Otherwise...
        else
        {
            // ... disable the nav mesh agent.
            nav.enabled = false;
            enabled=false;
        }
    } 
  
}

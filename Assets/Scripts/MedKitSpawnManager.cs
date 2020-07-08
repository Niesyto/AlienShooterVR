using UnityEngine;
using System.Collections;

/** @brief Manager of MedKit spawners */
public class MedKitSpawnManager : MonoBehaviour
{
    /** Reference to the player's heatlh. */
    public PlayerHealth playerHealth;      
    /** The enemy prefab to be spawned. */
    public GameObject medKit;                
    /** How long between each spawn. */
    public float spawnTime = 15f;            
    /** An array of the spawn points MedKit can spawn from. */
    public Transform[] spawnPoints;         

    /** @brief Call the Spawn function on start of the game */
    void Start ()
    {
        // Call the Spawn function
        StartCoroutine(Spawn(spawnTime) ) ;
    }

    /** @brief Spawn a MedKit
    @param time Time to wait between spawning a next MedKit */
    IEnumerator Spawn( float time )
    {
        yield return new WaitForSeconds(time) ;
        while( true )
        {
            if (playerHealth.currentHealth <= 0f)
            {    
                // ... exit the function.
                break;
            }
            // Find a random index between zero and one less than the number of spawn points.
            int spawnPointIndex = Random.Range (0, spawnPoints.Length);

            // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
            Instantiate (medKit, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
            yield return new WaitForSeconds(spawnTime) ;
        }
    }
}
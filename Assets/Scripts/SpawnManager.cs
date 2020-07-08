using UnityEngine;
using System.Collections;

/** @brief Manager of enemy spawners */
public class SpawnManager : MonoBehaviour
{
    /** Reference to the player's heatlh. */
    public PlayerHealth playerHealth;       
    /** The enemy prefab to be spawned. */
    public GameObject enemyFirst;
    /** The enemy prefab to be spawned. */
    public GameObject enemyBoss;
    /** The enemy prefab to be spawned. */
    public GameObject enemyFast;
    /** How long between each spawn. */
    public float spawnTime = 4f;          
    /** An array of the spawn points this enemy can spawn from. */
    public Transform[] spawnPoints;  
    /** Reference to zombie's health */       
    EnemyHealth enemyHealth;                
    /** Reference to zombie's attack */
    EnemyAttack enemyAttack;
    /** Reference to boss's health */
    EnemyHealth enemyBossHealth;
    /** Reference to boss's attack */
    EnemyAttack enemyBossAttack;
    /** Reference to fast zombie's health */
    EnemyHealth enemyFastHealth;
    /** Reference to fast zombie's attack */
    EnemyAttack enemyFastAttack;
    /** Variable used for dividing score to scale enemies */
    int scoreDivider;


    /** @brief Call the Spawn function on start of the game and set up references */
    void Start ()
    {
       
        // Call the Spawn function
        StartCoroutine(Spawn() ) ;

        scoreDivider = 100;

        enemyHealth = enemyFirst.GetComponent <EnemyHealth> ();
        enemyAttack = enemyFirst.GetComponent <EnemyAttack> ();

        enemyBossHealth= enemyBoss.GetComponent<EnemyHealth>();
        enemyBossAttack = enemyBoss.GetComponent<EnemyAttack>();


        enemyFastHealth = enemyFast.GetComponent<EnemyHealth>();
        enemyFastAttack = enemyFast.GetComponent<EnemyAttack>();

    }

    /** @brief Modify the spawn rate and enemy prefab according to player's score */
    void Update ()
    {
        // Modify spawn rate according to player's score
        spawnTime=4.0f - (ScoreManager.score/scoreDivider);
        if (spawnTime <= 0f)
        {
            // Upgrade enemies after reaching certain score
            spawnTime = 4f;
            scoreDivider = scoreDivider * 2;
            enemyHealth.scoreValue = enemyHealth.scoreValue * 2;
            enemyBossHealth.scoreValue = enemyBossHealth.scoreValue * 2;
            enemyFastHealth.scoreValue = enemyBossHealth.scoreValue * 2;

            enemyHealth.startingHealth = (int)(enemyHealth.startingHealth * 1.3);
            enemyAttack.attackDamage = (int)(enemyAttack.attackDamage * 1.3);

            enemyBossHealth.startingHealth = (int)(enemyBossHealth.startingHealth * 1.3);
            enemyBossAttack.attackDamage = (int)(enemyBossAttack.attackDamage * 1.3);

            enemyFastHealth.startingHealth = (int)(enemyFastHealth.startingHealth * 1.3);
            enemyFastAttack.attackDamage = (int)(enemyFastAttack.attackDamage * 1.3);
        }
    }

    /** @brief Spawn an enemy
    @param time Time to wait between spawning a next enemy */
    IEnumerator Spawn()
    {
        while( true )
        {
            if(playerHealth.currentHealth <= 0f)
            {
                // ... exit the function.
                break;
            }

            // Find a random index between zero and one less than the number of spawn points.
            int spawnPointIndex = Random.Range (0, spawnPoints.Length);

            // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
            Instantiate (enemyFirst, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);

            int chance = Random.Range(0, 99);
            if (ScoreManager.score > 500 && chance >= 50)
            {
                spawnPointIndex = Random.Range(0, spawnPoints.Length);
                Instantiate(enemyFast, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
            }


            chance = Random.Range(0, 99);
            //Create an instance of an enemy at random index location, if player has high enough score
            if (ScoreManager.score > 5000 && chance >= 90)
            {
                spawnPointIndex = Random.Range(0, spawnPoints.Length);
                
                Instantiate(enemyBoss, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
            }
            yield return new WaitForSeconds(spawnTime) ;

        }
    }


    /** @brief Reset the enemy preset */
    public void ResetEnemies()
    {
        enemyHealth.startingHealth=100;
        enemyAttack.attackDamage=10;
        enemyHealth.scoreValue=10;

        enemyBossAttack.attackDamage = 500;
        enemyBossHealth.startingHealth = 2000;
        enemyBossHealth.scoreValue = 500;

        enemyFastAttack.attackDamage = 5;
        enemyFastHealth.startingHealth = 50;
        enemyFastHealth.scoreValue = 20;
    }
}
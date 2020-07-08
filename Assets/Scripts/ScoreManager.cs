using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

/** @brief Managing the player's score */
public class ScoreManager : MonoBehaviour
{
    /** The player's score. */
    public static float score;       
    /** Score required do level up */
    public int scoreRequired;             
    /** Reference to the player */
    GameObject player;
    /** Reference to the Text component. */
    TextMeshProUGUI text;          
    /** Reference to the player's health. */
    PlayerHealth playerHealth;      
    /** Reference to the player's shooting script. */
    PlayerShooting playerShooting;   

    /** @brief Set up the references and starting score */
    void Awake ()
    {
        // Set up the reference.
        text = GetComponent <TextMeshProUGUI> ();
        player=GameObject.FindGameObjectWithTag("Player");

        playerHealth = player.GetComponent <PlayerHealth> ();
        playerShooting = player.GetComponentInChildren<PlayerShooting>();

        // Reset the score.
        score = 0;
        scoreRequired=50;
    
    }

    /** @brief Set the displayed text to the current score and run a CheckLevel() */
    void Update ()
    {
        // Set the displayed text to be the word "Score" followed by the score value.
        text.text = score.ToString();
        CheckLevel();
    }

    /** @brief Check if the player has leveled up */
    void CheckLevel()
    {
        if(score>=scoreRequired)
        {
            scoreRequired*=(int)2.5;
            IncreaseMaxHealth();
            IncreaseDamage();
        }
    }


    /** @brief Increase player's maximum health*/
    public void IncreaseMaxHealth()
    {
        playerHealth.startingHealth=(int)(playerHealth.startingHealth*1.2);
        playerHealth.currentHealth=(int)(playerHealth.currentHealth*1.2);
    }

    /** @brief Increase player's damage*/
    public void IncreaseDamage()
    {
        playerShooting.damagePerShot=(int)(playerShooting.damagePerShot*1.2f);
    }

}
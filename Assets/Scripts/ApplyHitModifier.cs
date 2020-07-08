using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ApplyHitModifier : MonoBehaviour
{
    // Damage multiplier for hits
    public float damageMultiplier = 1.0f;
    // Is hit object a leg 
    public bool isLeg = false;
    // Is hit object an arm
    public bool isArm = false;

    // Reference to health component
    EnemyHealth enemyHealth;
    // Reference to navigation component
    UnityEngine.AI.NavMeshAgent nav;
    // Reference to attacking component
    EnemyAttack enemyAttack;

    void Awake()
    {
        //Set up the references
        enemyHealth = GetComponentInParent<EnemyHealth>();
        enemyAttack = GetComponentInParent<EnemyAttack>();
        nav = GetComponentInParent<UnityEngine.AI.NavMeshAgent>();
    }

    // Apply additional effects to hits
    public void ModifyHit(int damage, Vector3 hitDirection)
    {
        // Calculate damage taken and cast it to float
        int takenDmg = (int)(damageMultiplier * damage);

        // Enemy takes damage
        enemyHealth.TakeDamage(takenDmg, hitDirection);

        // If collider hit is a leg
        if (isLeg)
            // Slow down the enemy
            nav.speed = (float)(nav.speed * 0.85);
        // If collider hit is an arm
        else if (isArm)
            // Lower enemy damage
            enemyAttack.attackDamage = (int)(enemyAttack.attackDamage * 0.85);

    }
}
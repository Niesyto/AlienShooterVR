using UnityEngine;

/** @brief Players shooting script */
public class PlayerShooting : MonoBehaviour
{
    /** The damage inflicted by each bullet. */
    public int damagePerShot = 50;                 
    /** The time between each shot. */
    public float timeBetweenBullets = 0.1f;       
    /** The distance the gun can fire. */
    public float range = 100f;                     
    /** A timer to determine when to fire. */
    float timer;                                   
    /** A ray from the gun end forwards. */
    Ray shootRay;                                  
    /** A raycast hit to get information about what was hit. */
    RaycastHit shootHit;                           
    /** A layer mask so the raycast only hits things on the shootable layer. */
    int shootableMask;                             
    /** Reference to the line renderer. */
    LineRenderer gunLine;                          
    /** Reference to the audio source. */
    AudioSource gunAudio;                          
    /** Reference to the light component. */
    Light gunLight;                                
    /** The proportion of the timeBetweenBullets that the effects will display for. */
    float effectsDisplayTime = 0.2f;               
    /** Reference to the particle renderer */
    ParticleSystem gunParticles;
    public bool isLeftHand;

    public Rigidbody projectile;
    public float speed = 20;


   
   /** @brief Set up references and create layer mask */
    void Awake ()
    {
        // Create a layer mask for the Shootable layer.
        shootableMask = LayerMask.GetMask ("Shootable");
        // Set up the references.
        gunLine = GetComponentInParent <LineRenderer> ();
        gunAudio = GetComponentInParent<AudioSource> ();
        gunLight = GetComponentInParent<Light> ();
        gunParticles = GetComponentInChildren<ParticleSystem> ();
       
        
    }

    /** @brief Update the timer, check if the player wants to shoot  */
    void Update ()
    {
        // Add the time since Update was last called to the timer.
        timer += Time.deltaTime;
        bool bDown=false;

        if (isLeftHand)
        {
            bDown = OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch);  
        }
       else
         {
            bDown = OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch);
         }

        // If the Fire1 button is being press and it's time to fire...
        if (bDown && timer>=timeBetweenBullets)
       {
            // ... shoot the gun.
            Shoot ();      
        }

        // If the timer has exceeded the proportion of timeBetweenBullets that the effects should be displayed for...
        if(timer >= timeBetweenBullets * effectsDisplayTime)
        {
            // ... disable the effects.
            DisableEffects ();       
        }
    }

    /** @brief Disable light and particle effects */
    public void DisableEffects ()
    {
        // Disable the line renderer and the light.
        gunLine.enabled = false;
        gunLight.enabled = false; 
    }

    /** @brief Fire the gun */
    public void Shoot()
    {

        // Reset the timer.
        timer = 0f;

        // Play the gun shot audioclip.
        gunAudio.Play();

        // Enable the light.
        gunLight.enabled = true;
        gunLine.enabled = true;
        gunParticles.Play();

        // Create a bullet projectile
        Rigidbody instantiatedProjectile = Instantiate(projectile, transform.position, transform.rotation) as Rigidbody;
        // Apply velocity to projectile
        instantiatedProjectile.velocity = transform.TransformDirection(new Vector3(0, 0, speed));


        // Enable the line renderer and set it's first position to be the end of the gun.
        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);

        // Set the shootRay so that it starts at the end of the gun and points forward from the barrel.
        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        var hitDirection = transform.rotation.eulerAngles;

        // Perform the raycast against gameobjects on the shootable layer and if it hits something...
        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            // Try and find an ApplyHitModifier script in the gameobject hit parents.
            ApplyHitModifier hitModifier = shootHit.collider.GetComponent<ApplyHitModifier>();

            // If the ApplyHitModifier component exist...
            if (hitModifier != null)
            {
                // ... the enemy should take damage.
                hitModifier.ModifyHit(damagePerShot, hitDirection);
            }

            // Set the second position of the line renderer to the point the raycast hit.
            gunLine.SetPosition(1, shootHit.point);

        }
        // If the raycast didn't hit anything on the shootable layer...
        else
        {
            // ... set the second position of the line renderer to the fullest extent of the gun's range.
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }

    }
}

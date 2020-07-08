using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** @brief Player movement script */
public class Movement : MonoBehaviour
{
    /** The speed that the player will move at. */
    public float speed = 1f;          
    /** The vector to store the direction of the player's movement. */
    Vector3 movement;                 
    /** Reference to the animator component. */
    Animator anim;                     
    /** Reference to the player's rigidbody. */
    Rigidbody playerRigidbody;         
    /** A layer mask so that a ray can be cast just at gameobjects on the floor layer. */
    int floorMask;                     
    /** The length of the ray from the camera into the scene. */
    float camRayLength = 100f;         
    /** Reference to audio player */
    AudioSource playerAudio;          

    /** @brief Set up references */
    void Awake()
    {
        // Create a layer mask for the floor layer.
        floorMask = LayerMask.GetMask("Floor");

        // Set up references.
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();
    }

    /** @brief Update player movement, rotation and animation */
    void FixedUpdate()
    {
        // Store the input axes.
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // Move the player around the scene.
        Move(h, v);

        // Turn the player to face the mouse cursor.
        Turning();

        // Animate the player.
        Animating(h, v);

        // Playing the sound
        PlayFootsteps();

    }

    /** @brief Change the player's position
    @param h Horizontal position offset
    @param v Vertical possition offset
     */
    void Move(float h, float v)
    {
        // Set the movement vector based on the axis input.
        movement.Set(h, 0f, v);

        // Normalise the movement vector and make it proportional to the speed per second.
        movement = movement.normalized * speed * Time.deltaTime;

        // Move the player to it's current position plus the movement.
        playerRigidbody.MovePosition(transform.position + movement);


    }

    /** @brief Turn the player to the mouse position */
    void Turning()
    {
        // Create a ray from the mouse cursor on screen in the direction of the camera.
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Create a RaycastHit variable to store information about what was hit by the ray.
        RaycastHit floorHit;

        // Perform the raycast and if it hits something on the floor layer...
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            // Create a vector from the player to the point on the floor the raycast from the mouse hit.
            Vector3 playerToMouse = floorHit.point - transform.position;

            // Ensure the vector is entirely along the floor plane.
            playerToMouse.y = 0f;

            // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

            // Set the player's rotation to this new rotation.
            playerRigidbody.MoveRotation(newRotation);
        }
    }

    /** @brief Check if the player is moving and animate it
    @param h Horizontal position offset
    @param v Vertical possition offset
     */
    void Animating(float h, float v)
    {
        // Create a boolean that is true if either of the input axes is non-zero.
        bool running = h != 0f || v != 0f;

        // Tell the animator whether or not the player is walking.
        anim.SetBool("IsRunning", running);

        if(Input.GetButton ("Fire1"))
            anim.SetBool("IsShooting",true);
        else
            anim.SetBool("IsShooting",false);

    }

    /** @brief Play footstep sounds */
    void PlayFootsteps()
    {
        if(anim.GetBool("IsRunning")==true)
        {
            // Play the movement sound
            playerAudio.pitch = (Random.Range(0.5f, 1.5f));
            playerAudio.enabled = true;
            playerAudio.loop = true;
        }
        if (anim.GetBool("IsRunning") == false)
        {
            playerAudio.enabled = false;
            playerAudio.loop = false;
        }
    }

    

}

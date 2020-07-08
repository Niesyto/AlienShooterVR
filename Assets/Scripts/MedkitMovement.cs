using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** @brief Movement of the MedKit  */
public class MedkitMovement : MonoBehaviour
{
    /** Time counter  */
    float timer;
    /** Amplitude of the vertical movement of the box  */
    float amplitude = 0.2f;
    /** Step of the movement  */
    public float step;
    /** Counter of movement steps  */
    float stepCounter;
    /** Rotation step  */
    public float rotationStep;
  

    /** @brief Update the object position and rotation  */
    void Update()
    {

        if (stepCounter >= amplitude || stepCounter <= -amplitude)
        {
            step *= (-1);
        }
        transform.position += new Vector3(0, step, 0);
        stepCounter += step;
        transform.Rotate(0, rotationStep, 0);
    }
}

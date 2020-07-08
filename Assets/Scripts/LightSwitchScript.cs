using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 /** @brief Operating the lights  */
public class LightSwitchScript : MonoBehaviour
{
    /** Reference to the trigger  */
    public GameObject trigger;
    /** Lights being turned on by the button  */
    public GameObject lights;
    /** Time counter  */
    float timer;
    /** Delay betweer each use of the button  */
    float switchDelay = 0.5f;
    /** Variable deciding whether the lights are on or off  */
    bool switchOn = true;

    /** @brief Turn on the light 
    @param other Object inside the collider
      */
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && Input.GetKey(KeyCode.E) && timer >= switchDelay) 
        {
            timer = 0f;
            switchOn = !switchOn;
            for (int i = 0; i < lights.transform.childCount; i++)
            {
                lights.transform.GetChild(i).gameObject.GetComponent<Light>().intensity = switchOn ? 1 : 0;
            }
        }

    }


    /** @brief Update the timer  */
    void Update()
    {
        timer += Time.deltaTime;
    }
}

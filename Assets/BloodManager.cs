using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodManager : MonoBehaviour
{
    /** Was the character damaged **/
    public static bool damaged = false;
    public float damageFlashTime = 2.0f;
    public RawImage DamageImage;
    public Color damageFlashColor;

    // Update is called once per frame
    void Update()
    {
        DamageFlash();
    }

    public void DamageFlash()
    {
        // If player got damaged
        if(damaged)
        {
            // Flash color
            DamageImage.color = damageFlashColor;
        }
        else
        {
            // Fade the flashed color
            DamageImage.color = Color.Lerp(DamageImage.color,Color.clear,damageFlashTime *Time.deltaTime);
        }
        //  Reset damaged flag
        damaged = false;
    }
}

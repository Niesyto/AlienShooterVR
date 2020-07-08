using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DeathHUDManager : MonoBehaviour
{
    /** Reference to the Text component. */
    TextMeshProUGUI text;
    Image image;
    public static bool isDead = false;

    /** Run method once the component is loaded **/
    void Awake()
    {
        image = GetComponentInChildren<Image>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    void Update()
    {
        if(isDead)
        {
            setDeadUI();
        }
    }

    // Update is called once per frame
    public void setDeadUI()
    {
        image.enabled = true;
        text.enabled = true;
    }
}

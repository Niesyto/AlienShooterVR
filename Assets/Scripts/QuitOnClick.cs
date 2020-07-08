using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** @brief Script for quitting the game */
public class QuitOnClick : MonoBehaviour
{
    /** @brief Quit the game */
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

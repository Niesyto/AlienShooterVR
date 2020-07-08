using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/** @brief Class responsible for loading the scene on pressing the button  */
public class LoadSceneOnClick : MonoBehaviour
{
    /** @brief Load the next scene
    @param sceneIndex Index of the scene to be loaded
      */
    public void LoadByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}

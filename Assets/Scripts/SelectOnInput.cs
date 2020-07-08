using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/** @brief Using keybord for selection in main menu */
public class SelectOnInput : MonoBehaviour
{
    /** Event system handling button pressing */
    public EventSystem eventSystem;
    /** Object currently selected */
    public GameObject selectedObject;
    /** Button selected */
    private bool buttonSelected;


    /** @brief Update the selected object */
    void Update()
    {
        if(Input.GetAxisRaw("Vertical") != 0 && buttonSelected == false)
        {
            eventSystem.SetSelectedGameObject(selectedObject);
            buttonSelected = true;
        }
    }

    /** @brieg Disable the object when something else is selectes */
    private void OnDisable()
    {
        buttonSelected = false;
    }
}

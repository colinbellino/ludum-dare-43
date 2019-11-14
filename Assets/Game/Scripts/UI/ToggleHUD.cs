using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleHUD : MonoBehaviour
{
    private void OnEnable()
    {
        this.AddObserver(ToggleHUDDisplay, KnowledgeSacrifice.OnUiDisable);   
    }

    private void ToggleHUDDisplay(object sender, object args)
    {
        gameObject.SetActive((bool) args);
    }

    private void OnDisable()
    {
        this.RemoveObserver(ToggleHUDDisplay, KnowledgeSacrifice.OnUiDisable);
    }
}

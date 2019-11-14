using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnowledgeSacrifice : SacrificeBehaviour
{
    public const string OnUiDisable = "KnowledgeSacrifice.OnUiDisable";

    protected void OnEnable()
    {
        this.PostNotification(OnUiDisable, false);
    }

    // protected void OnDisable()
    // {
    //    this.PostNotification(OnUiDisable, true);
    // }
}

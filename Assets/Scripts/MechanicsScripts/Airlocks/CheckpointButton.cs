using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointButton : Interactable
{
    public override void OnInteract()
    {
        base.OnInteract();

        SaveManager.Instance.Save();
    }
}

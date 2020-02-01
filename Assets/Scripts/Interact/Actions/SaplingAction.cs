using System.Collections;
using System.Collections.Generic;
using Interact;
using UnityEngine;

public class SaplingAction : UsableAction
{
    public override void Use()
    {
        base.Use();
        Debug.Log("sapling action");
        var obj = PlayerController.Instance.Drop();
        obj.CanBeGrabbedByPlayer = false;
        gameObject.GetComponentInChildren<TreeGrow>().enabled = true;
    }
}

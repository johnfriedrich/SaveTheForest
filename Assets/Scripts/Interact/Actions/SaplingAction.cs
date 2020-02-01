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
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        gameObject.GetComponent<Grabable>().CanBeGrabbedByPlayer = false;
        gameObject.GetComponentInChildren<TreeGrow>().enabled = true;
    }
}

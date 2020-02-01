using System.Collections;
using System.Collections.Generic;
using Interact;
using UnityEngine;

public class SaplingAction : UsableAction
{
    public override void Use()
    {
        base.Use();
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Grabable>().CanBeGrabbedByPlayer = false;
    }
}

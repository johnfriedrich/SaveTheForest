using Interact;
using Manager;
using UnityEngine;

public class FireAction : UsableAction
{
    public override void Use()
    {
        base.Use();
        EventManager.Instance.ProblemSolved(GetComponent<Grabable>());
        Debug.Log(GetComponent<Grabable>().Type);
        LevelManager.Instance.ProblemSolved(Problem.Fire);
        Destroy(gameObject);
    }
}

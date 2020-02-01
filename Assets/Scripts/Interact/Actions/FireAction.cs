using Interact;
using Manager;

public class FireAction : UsableAction
{
    public override void Use()
    {
        base.Use();
        EventManager.Instance.ProblemSolved(GetComponent<Grabable>());
        LevelManager.Instance.ProblemSolved(Problem.Fire);
        Destroy(gameObject);
    }
}

using Interact;

public class FireAction : UsableAction
{
    public override void Use()
    {
        base.Use();
        LevelManager.Instance.ProblemSolved(Problem.Fire);
        Destroy(gameObject);
    }
}

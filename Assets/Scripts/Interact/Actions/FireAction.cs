using Interact;

public class FireAction : UsableAction
{
    public override void Use()
    {
        base.Use();
        Destroy(gameObject);
    }
}

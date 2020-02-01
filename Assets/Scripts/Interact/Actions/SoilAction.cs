using UnityEngine;

namespace Interact.Actions
{
    public class SoilAction : UsableAction
    {
        public void Activate()
        {
            gameObject.SetActive(true);
        }

        public override void Use()
        {
            base.Use();
            Debug.Log("started soild action");
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}

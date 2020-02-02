using UnityEngine;

namespace Interact.Actions
{
    public class KoalaAction : UsableAction
    {
        [SerializeField] private Animator _animator;

        public override void Use()
        {
            base.Use();
            _animator.SetBool("wave", true);
        }

        public void Sit()
        {
            _animator.SetBool("wave", false);
            _animator.SetBool("sit", true);
        }
    }
}
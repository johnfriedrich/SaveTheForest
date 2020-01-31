using UnityEngine;

namespace Interact
{
    public class UsableAction : MonoBehaviour {
        public virtual void Take() {}

        public virtual void Use() {}
    
        public virtual void Help(){}

        public virtual void Damage(){}
    }
}
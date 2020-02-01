using Enums;
using UnityEngine;

namespace Interact
{
    public class Grabable : MonoBehaviour {

        public InteractableEnum Type;

        public InteractableEnum RequiredType;
        
        public UsableAction UsableAction;

        public bool CanBeGrabbedByPlayer;

    }
}
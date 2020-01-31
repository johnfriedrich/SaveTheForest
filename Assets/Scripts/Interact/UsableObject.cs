using System.Collections.Generic;
using System.Linq;
using Enums;
using UnityEngine;
using Enumerable = System.Linq.Enumerable;

namespace Interact
{
    public class UsableObject : MonoBehaviour {
        [SerializeField]
        protected AudioSource _soundPlayer;

        [SerializeField] private Transform _fireTransform;
        [SerializeField] private Transform _animalTransform;

        [SerializeField] private List<Grabable> _grabables = new List<Grabable>();

        public bool IsOnFire => Enumerable.Any(_grabables, grabable => grabable.Type == InteractableEnum.Fire);
        
        public bool HasNeedy => Enumerable.Any(_grabables, grabable => grabable.Type == InteractableEnum.Koala);

        private void Start()
        {
            var grabables = GetComponentsInChildren<Grabable>();
            for (int i = 0; i < grabables.Length; i++)
            {
                _grabables.Add(grabables[i]);
            }
        }

        public virtual Grabable TryHelp(Grabable otherGrabable)
        {
            var canHelp = false;
            Grabable grabToRemove = null;
            foreach (var thisGrabable in _grabables.Where(thisGrabable => thisGrabable.RequiredType == otherGrabable.Type && !canHelp))
            {
                if (otherGrabable.UsableAction) otherGrabable.UsableAction.Use();
                grabToRemove = thisGrabable;
                thisGrabable.UsableAction.Use();
                canHelp = true;
            }

            if (canHelp)
            {
                if (grabToRemove.Type != InteractableEnum.Water)
                {
                    _grabables.Remove(grabToRemove);
                }
                return grabToRemove;
            }

            return null;
        }

        //Called to set objects on Fire or puts koalas
        public bool TryPut(Grabable grabable)
        {
            if (!IsOnFire && grabable.Type == InteractableEnum.Fire)
            {
                _grabables.Add(grabable);
                grabable.gameObject.transform.SetParent(_fireTransform, false);
                return true;
            }
            if (!HasNeedy && grabable.Type == InteractableEnum.Koala)
            {
                _grabables.Add(grabable);
                grabable.gameObject.transform.SetParent(_animalTransform, false);
                return true;
            }
            return false;
        }

    }
}
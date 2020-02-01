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

        public InteractableEnum Type;

        [SerializeField] private Transform _fireTransform;
        [SerializeField] private Transform _animalTransform;

        [SerializeField] private List<Grabable> _grabables = new List<Grabable>();

        public bool IsOnFire => Enumerable.Any(_grabables, grabable => grabable.Type == InteractableEnum.Fire);
        
        public bool HasNeedy => Enumerable.Any(_grabables, grabable => grabable.Type == InteractableEnum.Koala);

        private void Start()
        {
            _grabables = GetComponentsInChildren<Grabable>().ToList();
        }

        public virtual Grabable TryHelp(Grabable otherGrabable)
        {
            var canHelp = false;
            Grabable grabToRemove = null;
            if (_grabables.Count == 0)
            {
                return null;
            }
            {
                
            }
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
                Put(grabable);
                return true;
            }
            if (!HasNeedy && grabable.Type == InteractableEnum.Koala)
            {
                Put(grabable);
                return true;
            }
            return false;
        }

        private void Put(Grabable grabable)
        {
            var clone = Instantiate(grabable);
            _grabables.Add(clone);
            if (clone.Type == InteractableEnum.Fire)
            {
                clone.gameObject.transform.SetParent(_fireTransform, false);
                return;
            }

            if (clone.Type == InteractableEnum.Koala)
            {
                clone.gameObject.transform.SetParent(_animalTransform, false);
                return;
            }
        }
    }
}
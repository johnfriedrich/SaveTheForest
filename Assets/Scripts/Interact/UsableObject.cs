﻿using System;
using System.Collections.Generic;
using System.Linq;
using Enums;
using Manager;
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

        public List<Grabable> Grabables => _grabables;

        public bool IsOnFire => Enumerable.Any(_grabables, grabable => grabable.Type == InteractableEnum.Fire);

        public bool HasNeedy => Enumerable.Any(_grabables, grabable => grabable.Type == InteractableEnum.Koala);

        private void Start()
        {
            _grabables = GetComponentsInChildren<Grabable>().ToList();
            if (Type == InteractableEnum.Tree)
            {
                var grab = _grabables.FirstOrDefault(grabable => grabable.Type == InteractableEnum.Stomp);
                if (grab != null)
                {
                    _grabables.Remove(grab);
                }
            }
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
                if (thisGrabable.UsableAction) thisGrabable.UsableAction.Use();
                canHelp = true;
            }

            if (canHelp)
            {
                if (grabToRemove.Type != InteractableEnum.Water && grabToRemove.Type != InteractableEnum.Sapling)
                {
                    _grabables.Remove(grabToRemove);
                }

                if (!grabToRemove.CanBeGrabbedByPlayer)
                {
                    return null;
                }
                EventManager.Instance.GrabableRemoved(grabToRemove);
                return grabToRemove;
            }

            return null;
        }

        //Called to set objects on Fire or puts koalas
        public bool TryPut(Grabable grabable)
        {
            if (!_fireTransform && !_animalTransform)
            {
                return false;
            }
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

        private void OnDestroy()
        {
            foreach (var _grabable in _grabables)
            {
                EventManager.Instance.GrabableDestroyed(_grabable);
            }
        }
    }
}
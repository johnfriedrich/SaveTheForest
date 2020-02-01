using System;
using UnityEngine;

namespace Interact.Actions
{
    public class SoilAction : UsableAction
    {

        [SerializeField] private Grabable _sapling;

        private void Start()
        {
        }

        public void Activate()
        {
            gameObject.SetActive(true);
        }

        public override void Use()
        {
            base.Use();
            Debug.Log("started soild action");
            _sapling.transform.SetParent(null);
            _sapling.gameObject.SetActive(true);
            _sapling.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            _sapling.gameObject.GetComponent<Grabable>().CanBeGrabbedByPlayer = false;
            _sapling.gameObject.AddComponent<TreeGrow>();
            Destroy(gameObject);
        }
    }
}

using System;
using UnityEngine;

namespace Interact.Actions
{
    public class StompAction : UsableAction
    {

        [SerializeField] private UsableObject _soil;

        private void Start()
        {
            _soil.gameObject.SetActive(false);
        }

        public override void Use()
        {
            base.Use();
            _soil.gameObject.SetActive(true);
            _soil.gameObject.transform.SetParent(null);
            _soil.gameObject.GetComponentInChildren<SoilAction>().Activate();
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}

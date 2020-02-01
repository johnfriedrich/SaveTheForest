using Enums;
using UnityEngine;

namespace Interact.Actions
{
    public class FillDrainAction : UsableAction
    {
        [SerializeField] private GameObject _water;

        private Grabable grabable;

        private void Start()
        {
            grabable = GetComponent<Grabable>();
            _water.SetActive(false);
        }

        public override void Use()
        {
            base.Use();
            if (!_water.activeSelf)
            {
                grabable.Type = InteractableEnum.FullWaterBucket;
                grabable.RequiredType = InteractableEnum.Fire;
                _water.SetActive(true);
            }
            else
            {
                grabable.Type = InteractableEnum.EmptyWaterBucket;
                grabable.RequiredType = InteractableEnum.Water;
                _water.SetActive(false);
            }
            
        }
    }
}

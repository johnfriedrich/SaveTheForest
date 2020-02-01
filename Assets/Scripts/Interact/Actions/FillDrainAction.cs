using Enums;
using UnityEngine;

namespace Interact.Actions
{
    public class FillDrainAction : UsableAction
    {
        [SerializeField] private GameObject _emptyBucket;
        [SerializeField] private GameObject _fullBucket;

        private Grabable grabable;

        private void Start()
        {
            grabable = GetComponent<Grabable>();
            _emptyBucket.SetActive(true);
            _fullBucket.SetActive(false);
        }

        public override void Use()
        {
            base.Use();
            if (_emptyBucket.activeSelf)
            {
                grabable.Type = InteractableEnum.FullWaterBucket;
                grabable.RequiredType = InteractableEnum.Fire;
                _fullBucket.SetActive(true);
                _emptyBucket.SetActive(false);
            }
            else
            {
                grabable.Type = InteractableEnum.EmptyWaterBucket;
                grabable.RequiredType = InteractableEnum.Water;
                _fullBucket.SetActive(false);
                _emptyBucket.SetActive(true);
            }
            
        }
    }
}

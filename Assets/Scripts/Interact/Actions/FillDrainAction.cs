using UnityEngine;

namespace Interact.Actions
{
    public class FillDrainAction : UsableAction
    {
        [SerializeField] private GameObject _emptyBucket;
        [SerializeField] private GameObject _fullBucket;

        private void Start()
        {
            _emptyBucket.SetActive(true);
            _fullBucket.SetActive(false);
        }

        public override void Use()
        {
            base.Use();
            if (_emptyBucket.activeSelf)
            {
                _fullBucket.SetActive(true);
                _emptyBucket.SetActive(false);
            }
            else
            {
                _fullBucket.SetActive(false);
                _emptyBucket.SetActive(true);
            }
            
        }
    }
}

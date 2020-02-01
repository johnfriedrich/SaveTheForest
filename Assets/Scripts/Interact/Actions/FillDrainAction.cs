using Enums;
using UnityEngine;

namespace Interact.Actions
{
    public class FillDrainAction : UsableAction
    {
        [SerializeField] private GameObject _water;

        AudioClip fillBucketClip;
        AudioClip pourBucketClip;

        AudioSource source;
        private Grabable grabable;

        private void Start()
        {
            grabable = GetComponent<Grabable>();
            _water.SetActive(false);
            source = GetComponent<AudioSource>();
        }

        public override void Use()
        {
            base.Use();
            if (!_water.activeSelf)
            {
                grabable.Type = InteractableEnum.FullWaterBucket;
                grabable.RequiredType = InteractableEnum.Fire;
                _water.SetActive(true);
                source.clip = fillBucketClip;
                source.Play();

            }
            else
            {
                grabable.Type = InteractableEnum.EmptyWaterBucket;
                grabable.RequiredType = InteractableEnum.Water;
                _water.SetActive(false);
                source.clip = pourBucketClip;
                source.Play();
            }
            
        }
    }
}

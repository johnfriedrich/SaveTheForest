using Enums;
using UnityEngine;

namespace Interact
{
    public class Grabable : MonoBehaviour
    {
        private float timer;
        [SerializeField] private float _cooldown = 10f;

        private AudioSource _audioSource;

        private void Start()
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
            _audioSource.playOnAwake = false;
        }

        private void Update()
        {
            if (timer < _cooldown)
            {
                timer += Time.deltaTime;
                
            }
            else
            {
                if (AudioClip != null)
                {
                    _audioSource.PlayOneShot(AudioClip);
                }

                timer = 0;
            }
        }

        public AudioClip AudioClip;
        
        public InteractableEnum Type;

        public InteractableEnum RequiredType;
        
        public UsableAction UsableAction;

        public bool CanBeGrabbedByPlayer;

    }
}
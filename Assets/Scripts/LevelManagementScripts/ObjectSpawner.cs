using System.Collections.Generic;
using System.Linq;
using Enums;
using Interact;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LevelManagementScripts
{
    public class ObjectSpawner : MonoBehaviour
    {
        [SerializeField] private List<UsableObject> _usableObjects;
        [SerializeField]
        private float animalSpawnTime;
        [SerializeField]
        private float fireSpawnTime;

        [SerializeField] private Grabable fire;
        [SerializeField] private Grabable animal;
        private float _fireTimer;
        private float _animalTimer;

        // Start is called before the first frame update
        private void Start()
        {
            _usableObjects = FindObjectsOfType<UsableObject>()
                .Where(usableObject => usableObject.Type == InteractableEnum.Tree).ToList();
        }

        private void Update()
        {
            if (_fireTimer < fireSpawnTime)
            {
                _fireTimer += Time.deltaTime;
            }
            else
            {
                if (SpawnGrabable(fire))
                {
                    LevelManager.Instance.ProblemSpawned(Problem.Fire);
                }
                _fireTimer = 0;
            }
            
            if (_animalTimer < animalSpawnTime)
            {
                _animalTimer  += Time.deltaTime;
            }
            else
            {
                if (SpawnGrabable(animal))
                {
                    LevelManager.Instance.ProblemSpawned(Problem.Animal);
                }
                _animalTimer = 0;
            }
        }


        private bool SpawnGrabable(Grabable grabable)
        {
            int i = 0;
            while (!_usableObjects[Random.Range(0, _usableObjects.Count)].TryPut(grabable) && i < _usableObjects.Count) i++;
            return i < _usableObjects.Count;
        }
    }
}

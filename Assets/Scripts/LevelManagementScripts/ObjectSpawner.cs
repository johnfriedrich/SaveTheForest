using System.Collections.Generic;
using System.Linq;
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
            _usableObjects = FindObjectsOfType<UsableObject>().ToList();
        }

        private void Update()
        {
            if (_fireTimer < animalSpawnTime)
            {
                _fireTimer += Time.deltaTime;
            }
            else
            {
                SpawnFire();
                _fireTimer = 0;
            }
            
            if (_animalTimer < fireSpawnTime)
            {
                _animalTimer  += Time.deltaTime;
            }
            else
            {
                SpawnAnimal();
                _animalTimer = 0;
            }
        }

        private bool SpawnFire()
        {
            var usableObject = _usableObjects[Random.Range(0, _usableObjects.Count - 1)];
            return usableObject.TryPut(fire);
        }

        private bool SpawnAnimal()
        {
            var usableObject = _usableObjects[Random.Range(0, _usableObjects.Count - 1)];
            return usableObject.TryPut(animal);
        }
    }
}

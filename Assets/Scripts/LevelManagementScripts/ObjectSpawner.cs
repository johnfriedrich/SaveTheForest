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
        [SerializeField]
        private float animalSpawnTime;
        [SerializeField]
        private float fireSpawnTime;

        [SerializeField] private Grabable fire;
        [SerializeField] private Grabable animal;
        private float _fireTimer;
        private float _animalTimer;

        // Start is called before the first frame update


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
            List<UsableObject> usableObjects = LevelManager.Instance.TreeObjects;
            int i = 0;
            while (!usableObjects[Random.Range(0, usableObjects.Count)].TryPut(grabable) && i < usableObjects.Count) i++;
            return i < usableObjects.Count;
        }
    }
}

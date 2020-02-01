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

        public static ObjectSpawner Instance => _instance;

        private static ObjectSpawner _instance;

        private void Awake()
        {
            _instance = this;
        }

        // Start is called before the first frame update


        public void AddUsable(UsableObject usableObject)
        {
            _usableObjects.Add(usableObject);
        }

        private void Update()
        {
            if (_fireTimer < fireSpawnTime)
            {
                _fireTimer += Time.deltaTime;
            }
            else
            {
                if (SpawnGrabable(Problem.Fire))
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
                if (SpawnGrabable(Problem.Animal))
                {
                    LevelManager.Instance.ProblemSpawned(Problem.Animal);
                }
                _animalTimer = 0;
            }
        }


        private bool SpawnGrabable(Problem problem)
        {
            Grabable grabable;
            if(problem == Problem.Animal)
            {
                grabable = animal;
            }
            else
            {
                grabable = fire;
            }
            List<UsableObject> usableObjects = LevelManager.Instance.TreeObjects;
            int i = 0;
            UsableObject tree;
            while (!(tree=usableObjects[Random.Range(0, usableObjects.Count)]).TryPut(grabable) && i < usableObjects.Count) i++;
            if (i < usableObjects.Count)
            {
                Navigation.Manager.AddElement(tree.gameObject.GetComponentInChildren<Grabable>().gameObject, problem);
            }
            return i < usableObjects.Count;
        }
    }
}

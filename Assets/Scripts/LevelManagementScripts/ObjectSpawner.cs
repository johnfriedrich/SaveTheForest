using System.Collections.Generic;
using System.Linq;
using Interact;
using Manager;
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
            LevelManager.Instance.TreeObjects.Add(usableObject);
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
            grabable = problem == Problem.Animal ? animal : fire;
            var usableObjects = LevelManager.Instance.TreeObjects;
            if (usableObjects.Count == 0)
            {
                 return false;
            }
            int i = 0;
            UsableObject tree;
            while (!(tree=usableObjects[Random.Range(0, usableObjects.Count)]).TryPut(grabable) && i < usableObjects.Count) i++;
            if (i < usableObjects.Count)
            {
                var newgrab = tree.Grabables.FirstOrDefault(ding => ding.Type == grabable.Type);
                Navigation.Manager.AddElement(newgrab.gameObject, problem);
            }
            EventManager.Instance.GrabableSpawned(grabable);
            return i < usableObjects.Count;
        }
    }
}

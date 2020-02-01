using Interact;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public enum Problem { Fire, Animal, Truck};

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    int[] _problems =  new int[2];
    public int MaxFires;
    public int MaxAnimals;

    public List<UsableObject> TreeObjects;



    public int Animals { get => _problems[1]; }
    public int Fires { get => _problems[0];  }

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        TreeObjects = FindObjectsOfType<UsableObject>()
            .Where(usableObject => usableObject.Type == InteractableEnum.Tree).ToList();
    }

    private void FixedUpdate()
    {
        CheckGameOver();
    }

    private void CheckGameOver()
    {
        if (_problems[0] >= MaxFires || _problems[1] >= MaxAnimals) GameOver();
    }

    private void GameOver()
    {
        //dostuff
    }

    public void ProblemSpawned(Problem problem)
    {
        _problems[(int)problem]++;
    }

    public void ProblemSolved(Problem problem)
    {
        _problems[(int)problem]--;
    }

}

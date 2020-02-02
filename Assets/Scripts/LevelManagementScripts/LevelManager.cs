using Interact;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using UnityEngine.SceneManagement;

public enum Problem { Fire, Animal, Truck};

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    public static LevelManager Instance;
    int[] _problems =  new int[2];
    public int MinTrees;
    public int KoalasSaved = 0;
    public int KoalaGoal;
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
        TreeObjects = FindObjectsOfType<UsableObject>().Where(usableObject => usableObject.Type == InteractableEnum.Tree).ToList();
    }


    private static void Win()
    {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(3);
    }

    private void FixedUpdate()
    {
        CheckGameOver();
        CheckWin();
    }

    private void CheckWin()
    {
        if (KoalasSaved >= KoalaGoal)
        {
            Win();
        }
    }

    private void CheckGameOver()
    {
        if (TreeObjects.Count < MinTrees) GameOver();
    }

    private void GameOver()
    {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(2);
    }

    public void ProblemSpawned(Problem problem)
    {
        _problems[(int)problem]++;
    }

    public void ProblemSolved(Problem problem)
    {
        if(problem == Problem.Animal)
        {
            KoalasSaved++;
        }
        _problems[(int)problem]--;

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Problem { Fire, Animal};

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    int[] _problems =  new int[2];

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


    public void ProblemSpawned(Problem problem)
    {
        _problems[(int)problem]++;
    }

    public void ProblemSolved(Problem problem)
    {
        _problems[(int)problem]--;
    }

}

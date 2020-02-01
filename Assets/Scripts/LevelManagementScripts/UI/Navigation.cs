using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Interact;

public class Navigation : MonoBehaviour
{
    GameObject Player;
    List<NavigationElement> elements;
    public static Navigation Manager;
    [SerializeField]
    GameObject koalaSpritePrefab;
    [SerializeField]
    GameObject fireSpritePrefab;

    private void Awake()
    {
        if (Manager)
        {
            Destroy(gameObject);
        }
        else
        {
            Manager = this;
        }
    }

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    public void RemoveElement(NavigationElement element)
    {
        elements.Remove(element);
    }

    public void AddElement(GameObject parent, Problem problem)
    {
        GameObject prefab;
        if(problem == Problem.Fire)
        {
            prefab = fireSpritePrefab;
        }
        else
        {
            prefab = koalaSpritePrefab;
        }
        elements.Add(new NavigationElement(parent.transform.position, prefab, parent));
    }
}

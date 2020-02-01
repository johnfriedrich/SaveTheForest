using System;
using System.Collections.Generic;
using System.Linq;
using Interact;
using Manager;
using UnityEngine;

public class Navigation : MonoBehaviour
{
    private GameObject _player;
    List<NavigationElement> elements;
    public static Navigation Manager;
    [SerializeField]
    GameObject koalaSpritePrefab;
    [SerializeField]
    GameObject fireSpritePrefab;
    [SerializeField]
    GameObject truckSpritePrefab;
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
        elements = new List<NavigationElement>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        EventManager.Instance.OnProblemSolvedEvent += RemoveElement;
    }

    public void RemoveElement(Grabable grabable)
    {
        NavigationElement delete = null;
        foreach (var element in elements)
        {
            if (element.ParentIsNull || !element.Type)
            {
                continue;
            }
            if (element.Type.Equals(grabable))
            {
                delete = element;
            }
        }

        if (delete)
        {
            elements.Remove(delete);
            delete.gameObject.SetActive(false);
        }
    }

    public void AddElement(GameObject parent, Problem problem)
    {
        GameObject prefab;
        switch (problem)
        {
            case Problem.Animal:
                prefab = koalaSpritePrefab;
                break;
            case Problem.Fire:
                prefab = fireSpritePrefab;
                break;
            case Problem.Truck:
                prefab = truckSpritePrefab;
                break;
            default:
                prefab = null;
                break;
        }
        var navigationElement = Instantiate(prefab, transform).GetComponent<NavigationElement>();
        navigationElement.FillNavigationElement(parent, _player);
        elements.Add(navigationElement);
    }
}

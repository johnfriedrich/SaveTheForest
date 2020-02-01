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
    [SerializeField]
    GameObject truckSpritePrefab;
    [SerializeField]
    GameObject NavElementPrefab;

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
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        foreach(NavigationElement element in elements)
        {
            element.UpdatePositionOnBar(Player.transform);
        }
    }

    public void RemoveElement(NavigationElement element)
    {
        elements.Remove(element);
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
        GameObject go = Instantiate(NavElementPrefab, transform);
        NavigationElement navigationElement = go.GetComponent<NavigationElement>();
        navigationElement.FillNavigationElement(prefab, parent);
        elements.Add(navigationElement);
    }
}

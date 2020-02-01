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
    }

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        elements = new List<NavigationElement>();
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
        if(problem == Problem.Fire)
        {
            prefab = fireSpritePrefab;
        }
        else
        {
            prefab = koalaSpritePrefab;
        }
        GameObject go = Instantiate(NavElementPrefab, transform);
        NavigationElement navigationElement = go.GetComponent<NavigationElement>();
        navigationElement.FillNavigationElement(prefab, parent);
        elements.Add(navigationElement);
    }
}

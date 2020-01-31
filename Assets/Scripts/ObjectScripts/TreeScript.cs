using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    public bool HasFire;
    public bool HasAnimal;

    [SerializeField]
    GameObject AnimalObject;
    [SerializeField]
    GameObject FireObject;

    public void StartFire()
    {
        if (!HasFire)
        {
            HasFire = true;
            FireObject.SetActive(true); 
        }
    }

    public void ExtinguishFire()
    {
        if (HasFire)
        {
            HasFire = false;
            FireObject.SetActive(false);
        }
    }

    public void SpawnAnimal()
    {
        if (!HasAnimal)
        {
            HasAnimal = true;
            AnimalObject.SetActive(true);
        }
    }

    public void TakeAnimal()
    {
        if (HasAnimal)
        {
            HasAnimal = false;
            AnimalObject.SetActive(false);
        }
    }

}

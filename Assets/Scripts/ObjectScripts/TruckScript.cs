using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Navigation.Manager.AddElement(gameObject, Problem.Truck);   
    }
}

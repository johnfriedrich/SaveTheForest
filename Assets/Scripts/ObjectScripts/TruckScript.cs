using System.Collections.Generic;
using Enums;
using Interact;
using Manager;
using UnityEngine;

public class TruckScript : MonoBehaviour
{
    private List<Grabable> _alreadyDeleviredGrabables = new List<Grabable>();
    
    // Start is called before the first frame update
    void Start()
    {
        Navigation.Manager.AddElement(gameObject, Problem.Truck);
        EventManager.Instance.OnGrabableSpawnedEvent += CheckIfInZone;
    }

    private void CheckIfInZone(Grabable grabable)
    {
        if (_alreadyDeleviredGrabables.Contains(grabable) || grabable.Type != InteractableEnum.Koala)
        {
            return;
        }

        if (!gameObject.GetComponent<BoxCollider>().bounds.Contains(grabable.transform.position))
        {
            return;
        }
        
        _alreadyDeleviredGrabables.Add(grabable);
        EventManager.Instance.ProblemSolved(grabable);
        LevelManager.Instance.ProblemSolved(Problem.Animal);
    }
}

using System.Collections;
using System.Collections.Generic;
using Enums;
using Interact;
using Manager;
using UnityEngine;

public class TruckScript : MonoBehaviour
{
    [SerializeField]
    private List<Grabable> _alreadyDeleviredGrabables = new List<Grabable>();
    
    // Start is called before the first frame update
    private void Start()
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
        grabable.CanBeGrabbedByPlayer = false;
        StartCoroutine(DisableCollider(grabable));
        EventManager.Instance.ProblemSolved(grabable);
        LevelManager.Instance.ProblemSolved(Problem.Animal);
    }

    private IEnumerator DisableCollider(Grabable grabable)
    {
        yield return new WaitForSecondsRealtime(2f);
        grabable.GetComponent<BoxCollider>().enabled = false;
        grabable.GetComponent<Rigidbody>().isKinematic = true;
    }
}

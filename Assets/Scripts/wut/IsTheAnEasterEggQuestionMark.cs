using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsTheAnEasterEggQuestionMark : MonoBehaviour
{
    [SerializeField]
    GameObject treeMesh;
    [SerializeField]
    GameObject cannon;
    BoxCollider boxCollider;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player") && Input.GetButtonDown("Fire2"))
        {
            Magic();
        }
    }

    private void Magic()
    {
        treeMesh.SetActive(false);
        cannon.SetActive(true);
        boxCollider.enabled = true;
    }
}

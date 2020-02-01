using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavigationElement : MonoBehaviour
{
    GameObject go;
    RectTransform rTransform;
    Vector3 position;
    GameObject _parent;


    public NavigationElement(Vector3 treePosition, GameObject prefab, GameObject parent)
    {
        _parent = parent;
        position = treePosition;
        go = Instantiate(prefab,transform);
        go.SetActive(true);
        rTransform = go.GetComponent<RectTransform>();
    }

    private void Update()
    {
        CheckParentDeath();
    }

    private void CheckParentDeath()
    {
        if (!_parent)
        {
            Destroy(go);
            Navigation.Manager.RemoveElement(this);
        }
    }

    public void UpdatePositionOnBar(Transform playerTransform)
    {
        Vector2 playerLookDirection2D = new Vector2(playerTransform.forward.x, playerTransform.forward.z);
        Vector3 playerToTree = position - playerTransform.position;
        Vector2 playerToTree2D = new Vector2(playerToTree.x, playerToTree.z);
        float angleBetween = Vector2.Angle(playerLookDirection2D, playerToTree2D);
        rTransform.anchoredPosition.Set(Mathf.Sin(angleBetween) * 200, 0);
    }
}

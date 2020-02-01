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


    public NavigationElement FillNavigationElement(Vector3 treePosition, GameObject prefab, GameObject parent)
    {
        _parent = parent;
        position = treePosition;
        go = Instantiate(prefab, transform.parent);
        go.SetActive(true);
        rTransform = go.GetComponent<RectTransform>();
        return this;
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
            Destroy(gameObject);
        }
    }

    public void UpdatePositionOnBar(Transform playerTransform)
    {
        Vector2 playerLookDirection2D = new Vector2(playerTransform.forward.x, playerTransform.forward.z);
        Vector3 playerToTree = position - playerTransform.position;
        Vector2 playerToTree2D = new Vector2(playerToTree.x, playerToTree.z).normalized;
        float angleBetween = Vector2.Angle(playerLookDirection2D, playerToTree2D);
        Vector2 directionalVec = playerToTree2D - playerLookDirection2D;
        if (directionalVec.x < 0) angleBetween *= -1;
        if (angleBetween > -70 && angleBetween < 70)
        {
            Vector2 pos = new Vector2(Mathf.Sin(angleBetween * Mathf.Deg2Rad) * -200, rTransform.anchoredPosition.y);
            rTransform.anchoredPosition = pos;
        }
        else if (angleBetween < -45)
        {
            rTransform.anchoredPosition = new Vector2(200, 0);
        }
        else
        {
            rTransform.anchoredPosition = new Vector2(-200, 0);
        }
    }
}

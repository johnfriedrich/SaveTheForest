using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Interact;
using Manager;

public class NavigationElement : MonoBehaviour
{
    GameObject go;
    RectTransform rTransform;
    GameObject _parent;
    bool beingCarried;
    Image _image;

    private void Start()
    {
        EventManager.Instance.OnGrabableRemovedEvent += OnPickUp;
        EventManager.Instance.OnGrabableSpawnedEvent += OnSetDown;
    }

    public NavigationElement FillNavigationElement(GameObject prefab, GameObject parent)
    {
        _parent = parent;
        go = Instantiate(prefab, transform.parent);
        go.SetActive(true);
        rTransform = go.GetComponent<RectTransform>();
        _image = go.GetComponent<Image>();
        return this;
    }

    private void Update()
    {
        if (beingCarried)
        {
            _image.enabled = false;
        }
        else
        {
            _image.enabled = true;
        }
    }

    private void CheckParentDeath()
    {
        if (!_parent)
        {
            Destroy(go);
            //Navigation.Manager.RemoveElement(this);
            //Destroy(gameObject);
        }
    }

    public void UpdatePositionOnBar(Transform playerTransform)
    {
        CheckParentDeath();
        if (!_parent)
        {
            return;
        }
        Vector2 playerLookDirection2D = new Vector2(playerTransform.forward.x, playerTransform.forward.z);
        Vector3 playerToTree = _parent.transform.position - playerTransform.position;
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

    private void OnPickUp(Grabable grabable)
    {
        if(grabable.gameObject == _parent)
        {
            beingCarried = true;
        }
    }

    private void OnSetDown(Grabable grabable)
    {
        if(grabable.gameObject == _parent)
        {
            beingCarried = false;
        }
    }
}

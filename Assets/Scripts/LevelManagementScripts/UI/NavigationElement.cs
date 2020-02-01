using UnityEngine;
using UnityEngine.UI;
using Interact;

public class NavigationElement : MonoBehaviour
{
    RectTransform rTransform;
    GameObject _parent;
    bool beingCarried;
    Image _image;
    private GameObject _player;

    public Grabable Type => _parent.GetComponent<Grabable>();

    public void FillNavigationElement(GameObject parent, GameObject player)
    {
        _parent = parent;
        gameObject.SetActive(true);
        rTransform = GetComponent<RectTransform>();
        _image = GetComponent<Image>();
        _player = player;
    }

    private void Update()
    {
        UpdatePositionOnBar();
        if (beingCarried)
        {
            _image.enabled = false;
        }
        else
        {
            _image.enabled = true;
        }
    }

    public void UpdatePositionOnBar()
    {
        if (_parent == null)
        {
            return;
        }
        Vector2 playerLookDirection2D = new Vector2(_player.transform.forward.x, _player.transform.forward.z);
        Vector3 playerToTree = _parent.transform.position - _player.transform.position;
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

using Interact;
using LevelManagementScripts;
using UnityEngine;

public class TreeGrow : MonoBehaviour
{
    private float timer;

    [SerializeField] private GameObject finalTree;
    [SerializeField] private GameObject sapling;
    
    private Vector3 _destinationScale = new Vector3(3.0f, 3.0f, 3.0f);
    [SerializeField]
    private float _growSpeed;

    private void Update()
    {
        Debug.Log("grwoth");
        while (timer < _growSpeed)
        {
            timer += Time.deltaTime;
            ScaleOverTime(timer);
        }

        if (sapling.transform.localScale == _destinationScale)
        {
            Destroy(sapling.gameObject);
            finalTree.transform.SetParent(null);
            finalTree.SetActive(true);
            ObjectSpawner.Instance.AddUsable(finalTree.GetComponent<UsableObject>());
            Destroy(gameObject);
        }
    }
    
    private void ScaleOverTime(float time)
    {
        Vector3 originalScale = sapling.transform.localScale;
        
        sapling.transform.localScale = Vector3.Lerp(originalScale, _destinationScale, time / _growSpeed);
    }
}

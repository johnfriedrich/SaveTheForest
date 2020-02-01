using UnityEngine;

public class TreeGrow : MonoBehaviour
{
    private float timer;

    [SerializeField] private GameObject finalTree;
    
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

        if (finalTree.transform.localScale == _destinationScale)
        {
            finalTree.SetActive(true);
            finalTree.transform.SetParent(null);
            Destroy(gameObject);
        }
    }
    
    private void ScaleOverTime(float time)
    {
        Vector3 originalScale = finalTree.transform.localScale;
        
        finalTree.transform.localScale = Vector3.Lerp(originalScale, _destinationScale, time / _growSpeed);
    }
}

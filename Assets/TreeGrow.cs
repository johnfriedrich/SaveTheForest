using System;
using UnityEngine;

public class TreeGrow : MonoBehaviour
{
    private float timer;

    [SerializeField] private GameObject finalTree;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer < 3)
        {
            ScaleOverTime(2);
            timer = 0f;
        }
    }
    
    private void ScaleOverTime(float time)
    {
        Vector3 originalScale = finalTree.transform.localScale;
        Vector3 destinationScale = new Vector3(3.0f, 3.0f, 3.0f);
         
        float currentTime = 0.0f;
        finalTree.transform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / time);
    }
}

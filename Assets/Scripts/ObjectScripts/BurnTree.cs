using System;
using Interact;
using UnityEngine;

namespace ObjectScripts
{
    public class BurnTree : MonoBehaviour
    {
        [SerializeField] private float burnDuration = 60.0f;
        [SerializeField] private UsableObject treeScript;
        [SerializeField] private GameObject stumpPrefab;

        private float timer;

        private void Start()
        {
            stumpPrefab.SetActive(false);
        }

        private void Update()
        {
            if (!treeScript.IsOnFire)
                timer = 0.0f;
            else if (treeScript.IsOnFire)
                timer += Time.deltaTime;

            if (timer >= burnDuration)
                SwitchTreeToStump();
        }

        private void SwitchTreeToStump()
        {
            stumpPrefab.SetActive(true);
            stumpPrefab.transform.SetParent(null);
            stumpPrefab.name = "Stump";
            
            Destroy(gameObject);
        }
    }
}
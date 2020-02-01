using System;
using UnityEngine;

namespace ObjectScripts
{
    public class BurnTree : MonoBehaviour
    {
        [SerializeField] private float burnDuration = 60.0f;
        [SerializeField] private TreeScript treeScript;
        [SerializeField] private GameObject stumpPrefab;

        private float timer;

        private void Update()
        {
            if (!treeScript.HasFire)
                timer = 0.0f;
            else if (treeScript.HasFire)
                timer += Time.deltaTime;

            if (timer >= burnDuration)
                SwitchTreeToStump();
        }

        private void SwitchTreeToStump()
        {
            GameObject stump = Instantiate(stumpPrefab, transform.position, Quaternion.identity);
            stump.name = "Stump";
            
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            timer = 0.0f;
            treeScript.HasAnimal = false;
            treeScript.HasFire = false;
        }
    }
}
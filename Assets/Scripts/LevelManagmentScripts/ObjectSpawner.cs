using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    TreeScript[] trees;
    [SerializeField]
    float animalSpawnTime;
    [SerializeField]
    float fireSpawnTime;

    // Start is called before the first frame update
    void Start()
    {
        trees = GetComponentsInChildren<TreeScript>();
        StartCoroutine(SpawnAnimal());
        StartCoroutine(SpawnFire());
    }


    IEnumerator SpawnFire()
    {
        trees[Random.Range(0, trees.Length)].StartFire();
        yield return new WaitForSeconds(fireSpawnTime);
        StartCoroutine(SpawnFire());
    }

    IEnumerator SpawnAnimal()
    {
        trees[Random.Range(0, trees.Length)].SpawnAnimal();
        yield return new WaitForSeconds(animalSpawnTime);
        StartCoroutine(SpawnAnimal());
    }
}

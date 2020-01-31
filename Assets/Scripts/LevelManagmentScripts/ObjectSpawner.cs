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
        TreeScript tree;
        int i = 0;
        while ((tree = trees[Random.Range(0, trees.Length)]).HasFire && i < trees.Length) i++;
        if(i < trees.Length) tree.StartFire();
        yield return new WaitForSeconds(fireSpawnTime);
        StartCoroutine(SpawnFire());
    }

    IEnumerator SpawnAnimal()
    {
        TreeScript tree;
        int i = 0;
        while ((tree = trees[Random.Range(0, trees.Length)]).HasAnimal && i < trees.Length) i++;
        if (i < trees.Length) tree.SpawnAnimal();
        yield return new WaitForSeconds(animalSpawnTime);
        StartCoroutine(SpawnAnimal());
    }
}

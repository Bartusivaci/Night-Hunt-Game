using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject skeletonPrefab;
    public float maxSkeletonInterval = 6f;


    void Start()
    {
        StartCoroutine(SpawnEnemies(skeletonPrefab, maxSkeletonInterval));
    }


    IEnumerator SpawnEnemies(GameObject prefab , float interval)
    {
        yield return new WaitForSeconds(Random.Range(2f, interval));
        GameObject newEnemy = Instantiate(prefab, transform.position, Quaternion.identity);
        StartCoroutine(SpawnEnemies(prefab, interval));
    }


}

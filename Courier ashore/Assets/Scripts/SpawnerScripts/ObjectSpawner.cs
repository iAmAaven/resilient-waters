using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public Transform objectParent;
    public GameObject[] objectPrefabs;
    public List<Transform> objectSpawnpoints = new List<Transform>();

    void Start()
    {
        SpawnObjects();
    }

    void SpawnObjects()
    {
        foreach (GameObject currentObject in objectPrefabs)
        {
            Transform randomSpawnpoint = objectSpawnpoints[Random.Range(0, objectSpawnpoints.Count)];
            Instantiate(currentObject, randomSpawnpoint.position, Quaternion.identity, objectParent);

            objectSpawnpoints.Remove(randomSpawnpoint);
        }
    }
}

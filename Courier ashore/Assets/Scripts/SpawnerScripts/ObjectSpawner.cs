using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public Transform objectParent;
    public GameObject[] objectPrefabs;
    public List<Transform> objectSpawnpoints = new List<Transform>();
    public bool isIslandSpawner = false;
    private IslandPackageManager islandPackageManager;
    void Start()
    {
        islandPackageManager = FindObjectOfType<IslandPackageManager>();
        SpawnObjects();
    }

    void SpawnObjects()
    {
        foreach (GameObject currentObject in objectPrefabs)
        {
            Transform randomSpawnpoint = objectSpawnpoints[Random.Range(0, objectSpawnpoints.Count)];
            GameObject newObject = Instantiate(currentObject, randomSpawnpoint.position, Quaternion.identity, objectParent);

            if (isIslandSpawner == true)
            {
                islandPackageManager.islands.Add(newObject);
            }

            objectSpawnpoints.Remove(randomSpawnpoint);
        }
    }
}

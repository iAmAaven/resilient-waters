using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandPackageManager : MonoBehaviour
{
    public List<GameObject> islands = new List<GameObject>();

    public void DistributePackageToIsland(string islandName, Package packageInfo)
    {
        foreach (GameObject island in islands)
        {
            if (island.gameObject.name.Contains(islandName))
            {
                island.GetComponentInChildren<IslandPickupPoint>().AddPackageToIsland(packageInfo);
            }
        }
    }

    public GameObject FindDestination(string islandName)
    {
        GameObject destinationIsland = null;

        foreach (GameObject island in islands)
        {
            if (island.gameObject.name.Contains(islandName))
            {
                destinationIsland = island;
            }
        }

        return destinationIsland;
    }
}
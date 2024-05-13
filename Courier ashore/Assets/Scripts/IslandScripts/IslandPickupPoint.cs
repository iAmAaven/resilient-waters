using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandPickupPoint : MonoBehaviour
{
    public Transform minPos, maxPos;
    public GameObject packageItemPrefab;
    private ReceiverNPCManager receiverNPCManager;

    public void AddPackageToIsland(Package package)
    {
        GameObject newPackageItem =
        Instantiate(packageItemPrefab, new Vector3(
        Random.Range(minPos.position.x, maxPos.position.x),
        Random.Range(minPos.position.y, maxPos.position.y), 0),
        Quaternion.identity, transform);

        newPackageItem.GetComponent<PackageItem>().packageInfo = package;
    }
    public GameObject SpawnReceiver(Package package, PackageItem packageForNPC)
    {
        GameObject npc;
        receiverNPCManager = FindObjectOfType<ReceiverNPCManager>();

        if (package.isContraband)
        {
            npc = receiverNPCManager.shadyCharacters[Random.Range(0, receiverNPCManager.shadyCharacters.Length)];
        }
        else
        {
            npc = receiverNPCManager.receiverCharacters[Random.Range(0, receiverNPCManager.receiverCharacters.Length)];
        }

        npc = Instantiate(npc, new Vector3(
        Random.Range(minPos.position.x, maxPos.position.x),
        Random.Range(minPos.position.y, maxPos.position.y), 0),
        Quaternion.identity, transform);

        Debug.Log(npc.gameObject.name);
        npc.GetComponent<ReceiverNPC>().packageForThisNPC = packageForNPC;

        return npc;
    }
}

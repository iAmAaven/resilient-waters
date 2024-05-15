using System.Collections;
using System.Collections.Generic;
using NavMeshPlus.Components;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshInformation : MonoBehaviour
{
    public NavMeshSurface navMeshSurface;

    public void RefreshNav()
    {
        navMeshSurface.UpdateNavMesh(navMeshSurface.navMeshData);
    }
}

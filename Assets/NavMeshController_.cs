using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class NavMeshController_ : MonoBehaviour
{
    [SerializeField]
    NavMeshSurface navmeshC;

    public void BakeNavMesh()
    {
        navmeshC.BuildNavMesh();
    }
}

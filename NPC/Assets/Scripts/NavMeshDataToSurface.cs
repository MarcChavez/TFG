using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshDataToSurface : MonoBehaviour
{
    public NavMeshData navMeshData; // Asigna tu NavMeshData en el Inspector

    void Start()
    {
        // Crea un nuevo objeto NavMeshSurface
        GameObject navMeshSurfaceObject = new GameObject("NavMeshSurface");
        NavMeshSurface navMeshSurface = navMeshSurfaceObject.AddComponent<NavMeshSurface>();

        // Asigna tu NavMeshData al NavMeshSurface
        navMeshSurface.navMeshData = navMeshData;

        // Opcionalmente, puedes configurar otros parámetros del NavMeshSurface aquí, como agentTypeID, buildHeightMesh, etc.

        // Llama al método BuildNavMesh() para que el NavMeshSurface genere la NavMesh
        navMeshSurface.BuildNavMesh();
    }
}

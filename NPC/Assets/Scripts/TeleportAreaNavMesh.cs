using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class TeleportAreaNavMesh : MonoBehaviour
{
    public NavMeshData navMeshData;
    public GameObject teleportAreaGameObject; // El GameObject que contiene tu Teleport Area

    void Start()
    {
        // Genera la NavMesh
        GameObject navMeshSurfaceObject = new GameObject("NavMeshSurface");
        NavMeshSurface navMeshSurface = navMeshSurfaceObject.AddComponent<NavMeshSurface>();

        // Asigna tu NavMeshData al NavMeshSurface
        navMeshSurface.navMeshData = navMeshData;


        // Llama al método BuildNavMesh() para que el NavMeshSurface genere la NavMesh
        navMeshSurface.BuildNavMesh();

        // Obtiene la Mesh de la NavMesh
        NavMeshTriangulation navMeshTriangulation = NavMesh.CalculateTriangulation();
        Mesh navMeshMesh = new Mesh
        {
            vertices = navMeshTriangulation.vertices,
            triangles = navMeshTriangulation.indices
        };

        // Asigna la Mesh de la NavMesh a tu Teleport Area
        MeshFilter teleportMeshFilter = teleportAreaGameObject.GetComponent<MeshFilter>();
        if (teleportMeshFilter != null)
        {
            teleportMeshFilter.mesh = navMeshMesh;
        }

        MeshCollider teleportMeshCollider = teleportAreaGameObject.GetComponent<MeshCollider>();
        if (teleportMeshCollider != null)
        {
            teleportMeshCollider.sharedMesh = navMeshMesh;
        }
    }
}

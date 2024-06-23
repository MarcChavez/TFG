#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshToMesh : MonoBehaviour
{
    [MenuItem("Tools/Generate NavMesh Mesh")]
    static void GenerateNavMeshMesh()
    {
        var triangulation = NavMesh.CalculateTriangulation();
        var mesh = new Mesh
        {
            vertices = triangulation.vertices,
            triangles = triangulation.indices
        };
        
        // Optionally optimize the mesh
        MeshUtility.Optimize(mesh);

        // Save the mesh as an asset
        AssetDatabase.CreateAsset(mesh, "Assets/NavMeshMesh.asset");
        AssetDatabase.SaveAssets();
        
        Debug.Log("NavMesh Mesh generated and saved as asset.");
    }
}
#endif

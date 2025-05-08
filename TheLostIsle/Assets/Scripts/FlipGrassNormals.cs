using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class FlipGrassNormals : MonoBehaviour
{
    void Start()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (meshFilter == null || meshFilter.mesh == null) return;

        Mesh mesh = meshFilter.mesh;

        mesh.RecalculateNormals();
        Vector3[] normals = mesh.normals;

        for (int i = 0; i < normals.Length; i++)
        {
            normals[i] = -normals[i]; // Invert normal direction
        }

        mesh.normals = normals;

        // Optional: flip triangle winding to match normals
        for (int subMesh = 0; subMesh < mesh.subMeshCount; subMesh++)
        {
            int[] triangles = mesh.GetTriangles(subMesh);
            for (int i = 0; i < triangles.Length; i += 3)
            {
                int temp = triangles[i];
                triangles[i] = triangles[i + 1];
                triangles[i + 1] = temp;
            }
            mesh.SetTriangles(triangles, subMesh);
        }
    }
}

using System.Collections.Generic;
using UnityEngine;

namespace ApplyYourself
{
    public class TerrainGenerator : MonoBehaviour 
    {
        [SerializeField] private MeshFilter filter = default;
        [SerializeField] private MeshCollider coll = default;

        [SerializeField] private int chunksCount = default;
        [SerializeField] private int chunkFidelity = default;
        [SerializeField] private MeshColliderCookingOptions options = default;

        private Mesh mesh;
        private int[] triangles;

       //  public void Write(ITerrainable terrainable) => MakeNewTerrain(terrainable);
       //  public void Write(Vector3[] verts) => UpdateMesh(verts);

        private void MakeNewTerrain()
        {
            mesh = new Mesh();
            filter.mesh = mesh;
            coll.cookingOptions = options;
            mesh.MarkDynamic();

            // ----

            Vector3[] verts = GenerateMesh(10, 10);
          //  listeners.ForEach(x => x.attached.Write(verts));
            UpdateMesh(verts);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Verts.</returns>
        private Vector3[] GenerateMesh(int width, int depth)
        {
            Vector3[] verticies = new Vector3[(width + 1) * (depth + 1)];
            int i = 0;
            float height = 0f;

            for (int z = 0; z <= width; z++)
            {
                for (int x = 0; x <= depth; x++)
                {
                    //terrainable.SetHeightStartup(x, ref height, z);
                    verticies[i] = new Vector3(x, height, z);

                    height = 0f;
                    i++;
                }
            }

            triangles = new int[width * depth * 6];

            int vert = 0;
            int tris = 0;

            for (int z = 0; z < width; z++)
            {
                for (int x = 0; x < depth; x++)
                {
                   // triangles[tris + 0] = vert + 0;
                   // triangles[tris + 1] = vert + terrainable.GetSize() + 1;
                   // triangles[tris + 2] = vert + 1;
                   //
                   // triangles[tris + 3] = vert + 1;
                   // triangles[tris + 4] = vert + terrainable.GetSize() + 1;
                   // triangles[tris + 5] = vert + terrainable.GetSize() + 2;

                    vert++;
                    tris += 6;
                }

                vert++;
            }

            return verticies;
        }

        private void UpdateMesh(Vector3[] verticies)
        {
            mesh.Clear();

            mesh.vertices = verticies;
            mesh.triangles = triangles;
            mesh.RecalculateNormals();

            Physics.BakeMesh(mesh.GetInstanceID(), false, options);
            coll.sharedMesh = mesh;

            //cameraPivot.attached.Write(Vector3.up * ((mesh.bounds.min.y + mesh.bounds.max.y) / 2f));
        }
    }
}
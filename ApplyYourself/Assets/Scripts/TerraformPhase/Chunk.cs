using UnityEngine;

namespace ApplyYourself
{
    public class Chunk : MonoBehaviour
    {
        /// <summary>
        /// not smart idgaf
        /// </summary>
        public Vector3[] verticies;

        [SerializeField] private MeshFilter filter = default;
        [SerializeField] private MeshCollider coll = default;
        [SerializeField] private MeshColliderCookingOptions options = default;
        private Mesh mesh;

        // add:
        // center of chunk transofmr
        // low graphics cube

        public void Setup(float[,] heightmap, int width, Vector3 offset, float spacing)
        {
            mesh = new Mesh();
            filter.mesh = mesh;
            coll.cookingOptions = options;
            mesh.MarkDynamic();

            verticies = GenerateVertData(heightmap, width, offset, spacing);
            mesh.triangles = GenerateTriData(width);
            ReloadMesh();
        }

        private Vector3[] GenerateVertData(float[,] heightmap, int width, Vector3 offset, float spacing)
        {
            Vector3[] verticies = new Vector3[(width + 1) * (width + 1)];

            int i = 0;
            for (int z = 0; z <= width; z++)
            {
                for (int x = 0; x <= width; x++)
                {
                    // also consider other chunks, i want all the verticies to be in worldspace.
                    verticies[i] = new Vector3(x * spacing, heightmap[x, z], z * spacing) + offset;
                    i++;
                }
            }

            return verticies;
        }

        private int[] GenerateTriData(int width)
        {
            int vert = 0;
            int tris = 0;
            int[] triangles = new int[width * width * 6];

            for (int z = 0; z < width; z++)
            {
                for (int x = 0; x < width; x++)
                {
                    triangles[tris + 0] = vert + 0;
                    triangles[tris + 1] = vert + width + 1;
                    triangles[tris + 2] = vert + 1;

                    triangles[tris + 3] = vert + 1;
                    triangles[tris + 4] = vert + width + 1;
                    triangles[tris + 5] = vert + width + 2;

                    vert++;
                    tris += 6;
                }

                vert++;
            }

            return triangles;
        }

        public void ReloadMesh()
        {
            mesh.vertices = verticies;
            // mesh.triangles = triangles;
            mesh.RecalculateNormals();

            Physics.BakeMesh(mesh.GetInstanceID(), false, options);
            coll.sharedMesh = mesh;

            // todo, calculate av and move the big ass cube here.
        }
    }
}

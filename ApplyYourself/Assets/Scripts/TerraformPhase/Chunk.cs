using UnityEngine;

namespace ApplyYourself
{
    public class Chunk : MonoBehaviour
    {
        /// <summary>
        /// not smart idgaf
        /// </summary>
        [HideInInspector] public Vector3[] verticies;

        [SerializeField] private MeshFilter filter = default;
        [SerializeField] private MeshCollider coll = default;
        [SerializeField] private MeshColliderCookingOptions options = default;
        private Mesh mesh;

        private TerrainManager manager;

        // add:
        // center of chunk transofmr
        // low graphics cube

        public void Setup(TerrainManager manager, int vertsAcrossChunk)
        {
            this.manager = manager;

            mesh = new Mesh();
            mesh.name = gameObject.name;
            filter.mesh = mesh;
            coll.cookingOptions = options;
            mesh.MarkDynamic();

            // save offset for lods

            verticies = GenerateVerts(vertsAcrossChunk);
            mesh.triangles = GenerateTris(vertsAcrossChunk);



            ReloadMesh();
        }

        private Vector3[] GenerateVerts(int width)
        {
            Vector3[] verticies = new Vector3[(width + 1) * (width + 1)];

            int i = 0;
            for (int x = 0; x <= width; x++)
            {
                for (int z = 0; z <= width; z++)
                {
                    // also consider other chunks, i want all the verticies to be in worldspace.
                    verticies[i] = new Vector3(x, 0f, z);
                    verticies[i].y = manager.GetHeight(x + transform.position.x, z + transform.position.z);
                    i++;
                }
            }

            return verticies;
        }

        private int[] GenerateTris(int width)
        {
            int[] triangles = new int[width * width * 6];

            int vert = 0;
            int tris = 0;
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

            // todo, calculate avpos for LODS and move the big ass cube here.
        }
    }
}

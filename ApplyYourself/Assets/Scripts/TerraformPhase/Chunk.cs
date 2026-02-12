using UnityEngine;

namespace ApplyYourself
{
    public class Chunk : MonoBehaviour
    {
        [HideInInspector] public Vector3[] verticies;
        
        [SerializeField] private MeshColliderCookingOptions options = default;
        [SerializeField] private float spaceBetweenVerts = default;
        [SerializeField] private int vertsAcross = default;

        private IHeightmap heightmap;
        private MeshFilter filter;
        private MeshCollider coll;
        private int[] triangles;
        private Mesh mesh;

        public void Setup(IHeightmap heightmap, Vector3 offset)
        {
            this.heightmap = heightmap;
            
            filter = GetComponent<MeshFilter>();
            coll = GetComponent<MeshCollider>();

            mesh = new Mesh();
            mesh.name = gameObject.name;
            filter.mesh = mesh;
            coll.cookingOptions = options;
            mesh.MarkDynamic();

            verticies = GenerateVerticies(vertsAcross, offset);
            triangles = GenerateTriangles(vertsAcross);
            mesh.vertices = verticies;
            mesh.triangles = triangles;
            ReloadMesh();
        }

        public void MoveUp() 
        {
            for (int i = 0; i < verticies.Length; i++)
            {
                verticies[i].y += 5f;
            }

            ReloadMesh();
        }

        private Vector3[] GenerateVerticies(int size, Vector3 offset)
        {
            Vector3[] verticies = new Vector3[(size + 1) * (size + 1)];

            int i = 0;
            for (int z = 0; z <= size; z++)
            {
                for (int x = 0; x <= size; x++)
                {
                    float worldX = x * spaceBetweenVerts + offset.x;
                    float worldZ = z * spaceBetweenVerts + offset.z;
                    verticies[i] = new Vector3(worldX, heightmap.GetHeight(worldX, worldZ), worldZ);
                    i++;
                }
            }

            return verticies;
        }

        private int[] GenerateTriangles(int size)
        {
            triangles = new int[size * size * 6];
            int vert = 0;
            int tris = 0;

            for (int z = 0; z < size; z++)
            {
                for (int x = 0; x < size; x++)
                {
                    triangles[tris + 0] = vert + 0;
                    triangles[tris + 1] = vert + size + 1;
                    triangles[tris + 2] = vert + 1;

                    triangles[tris + 3] = vert + 1;
                    triangles[tris + 4] = vert + size + 1;
                    triangles[tris + 5] = vert + size + 2;

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
            mesh.RecalculateNormals();

            Physics.BakeMesh(mesh.GetInstanceID(), false, options);
            coll.sharedMesh = mesh;
        }
    }
}

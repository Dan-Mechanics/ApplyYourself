using UnityEngine;

namespace ApplyYourself
{
    public class Chunk : MonoBehaviour
    {
        [SerializeField] private MeshFilter filter = default;
        [SerializeField] private MeshCollider coll = default;
        [SerializeField] private MeshColliderCookingOptions options = default;
        [SerializeField, Min(3)] private int size = default;
        private Mesh mesh;
        private int[] triangles;

        private void OnValidate()
        {
            Setup();
        }

        public void Setup()
        {
            mesh = new Mesh();
            filter.mesh = mesh;
            coll.cookingOptions = options;
            mesh.MarkDynamic();

            Vector3[] verts = GenerateVertData(size);
            SetVerticies(verts);
        }

        private Vector3[] GenerateVertData(int size)
        {
            Vector3[] verticies = new Vector3[(size + 1) * (size + 1)];

            int i = 0;
            for (int z = 0; z <= size; z++)
            {
                for (int x = 0; x <= size; x++)
                {
                    verticies[i] = new Vector3(x, Random.value * 10f, z);
                    i++;
                }
            }

            int vert = 0;
            int tris = 0;
            triangles = new int[size * size * 6];

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

            return verticies;
        }

        private void SetVerticies(Vector3[] verticies)
        {
            mesh.vertices = verticies;
            mesh.triangles = triangles;
            mesh.RecalculateNormals();

            Physics.BakeMesh(mesh.GetInstanceID(), false, options);
            coll.sharedMesh = mesh;
        }
    }
}

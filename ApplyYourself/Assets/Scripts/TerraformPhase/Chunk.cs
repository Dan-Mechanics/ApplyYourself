using UnityEngine;

namespace ApplyYourself
{
    public class Chunk : MonoBehaviour
    {
        [SerializeField] private MeshColliderCookingOptions options = default;
       // [SerializeField] private Texture2D texture = default;
      //  [SerializeField] private float height = default;
        [SerializeField] private float spaceBetweenVerts = default;
        [SerializeField] private int vertsAcross = default;
        //[SerializeField] private bool showInEditor = default;

        private MeshFilter filter;
        private MeshCollider coll;
        private int[] triangles;
        private Mesh mesh;

        private IHeightmapService heightmap;

        /*private void OnValidate()
        {
            if (showInEditor)
                Setup();

            showInEditor = false;
        }*/

        public void Setup(IHeightmapService heightmap)
        {
            this.heightmap = heightmap;
            
            filter = GetComponent<MeshFilter>();
            coll = GetComponent<MeshCollider>();

            mesh = new Mesh();
            filter.mesh = mesh;
            coll.cookingOptions = options;
            mesh.MarkDynamic();

            Vector3[] verts = GenerateVertData(vertsAcross);
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
                    verticies[i] = new Vector3(x * spaceBetweenVerts, 0f, z * spaceBetweenVerts);
                    verticies[i].y = heightmap.GetHeight(verticies[i].x + transform.position.x, verticies[i].z + transform.position.z);
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

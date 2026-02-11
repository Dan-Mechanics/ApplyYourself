using UnityEngine;

namespace ApplyYourself
{
    public class Chunk : MonoBehaviour
    {
        [SerializeField] private MeshFilter filter = default;
        [SerializeField] private MeshCollider coll = default;
        [SerializeField] private MeshColliderCookingOptions options = default;
        [SerializeField] private int fidelity = default;

        private Mesh mesh;
        private int[] triangles;
        private float size;

        //  public void Write(ITerrainable terrainable) => MakeNewTerrain(terrainable);
        //  public void Write(Vector3[] verts) => UpdateMesh(verts);

        public void Setup(float size)
        {
            this.size = size;
            
            mesh = new Mesh();
            filter.mesh = mesh;
            coll.cookingOptions = options;
            mesh.MarkDynamic();

            Vector3[] verts = GenerateVertData(fidelity);
            SetVerticies(verts);
        }

        private Vector3[] GenerateVertData(int fidelity, float size)
        {
            Vector3[] verticies = new Vector3[(fidelity + 1) * (fidelity + 1)];
            int i = 0;
            float height = 0f;

            for (int z = 0; z <= fidelity; z++)
            {
                for (int x = 0; x <= fidelity; x++)
                {
                    //terrainable.SetHeightStartup(x, ref height, z);
                    verticies[i] = new Vector3(x, height, z);

                    height = 0f;
                    i++;
                }
            }

            int vert = 0;
            int tris = 0;
            triangles = new int[fidelity * fidelity * 6];

            for (int z = 0; z < fidelity; z++)
            {
                for (int x = 0; x < fidelity; x++)
                {
                    triangles[tris + 0] = vert + 0;
                    triangles[tris + 1] = vert + fidelity + 1;
                    triangles[tris + 2] = vert + 1;
                    
                    triangles[tris + 3] = vert + 1;
                    triangles[tris + 4] = vert + fidelity + 1;
                    triangles[tris + 5] = vert + fidelity + 2;

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

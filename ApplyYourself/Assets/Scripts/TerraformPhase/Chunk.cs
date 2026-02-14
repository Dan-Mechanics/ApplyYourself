using System.Collections.Generic;
using UnityEngine;

namespace ApplyYourself
{
    public class Chunk : MonoBehaviour
    {
        [HideInInspector] public Vector3[] verticies;
        [HideInInspector] public float height;
        
        [SerializeField] private MeshColliderCookingOptions options = default;
        [SerializeField] private ChunkApproximation approximation = default;
        [SerializeField] private float spaceBetweenVerts = default;
        [SerializeField] private int vertsAcross = default;

        private MeshFilter filter;
        private MeshCollider coll;
        private int[] triangles;
        private Mesh mesh;

        public void Setup(IHeightmap heightmap, Vector3 offset, Transform cam)
        {
            filter = GetComponent<MeshFilter>();
            coll = GetComponent<MeshCollider>();
            approximation.Setup(spaceBetweenVerts * vertsAcross, cam, offset);

            mesh = new Mesh();
            mesh.name = gameObject.name;
            filter.mesh = mesh;
            coll.cookingOptions = options;
            mesh.MarkDynamic();

            verticies = GenerateVerticies(vertsAcross, heightmap, offset);
            triangles = GenerateTriangles(vertsAcross);

            mesh.vertices = verticies;
            mesh.triangles = triangles;
            ReloadMesh();
        }

        private Vector3[] GenerateVerticies(int size, IHeightmap heightmap, Vector3 offset)
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
            mesh.RecalculateBounds();
            UpdateHeight();

            Physics.BakeMesh(mesh.GetInstanceID(), false, options);
            coll.sharedMesh = mesh;
        }

        private void UpdateHeight()
        {
            int count = 0;
            float avHeight = 0f;
            int width = vertsAcross + 1;
            for (int i = 0; i < verticies.Length; i += width)
            {
                avHeight += verticies[i].y;
                count++;
            }

            avHeight /= count;
            height = avHeight;
            approximation.UpdateHeight(avHeight);
        }
    }
}

using System.Collections.Generic;
using UnityEngine;

namespace ApplyYourself
{
    [CreateAssetMenu(fileName = nameof(HeightBrush), menuName = nameof(HeightBrush))]
    public class HeightBrush : Brush
    {
        private TerrainManager terrainManager;
        public override void Setup()
        {
            base.Setup();
            terrainManager = FindAnyObjectByType<TerrainManager>();
        }

        public override void Apply(Vector3 point)
        {
            List<Chunk> neighbourChunks = terrainManager.GetChunksInProximity(point);
            foreach (Chunk chunk in neighbourChunks)
            {
                bool hasChanged = false;
                for (int i = 0; i < chunk.verticies.Length; i++)
                {
                    Vector3 vert = chunk.verticies[i];
                    if (Vector3.Distance(Utils.Flatten(vert), Utils.Flatten(point)) > size)
                        continue;

                    vert.y += strength * Time.fixedDeltaTime;
                    chunk.verticies[i] = vert;
                    hasChanged = true;
                }

                if (hasChanged)
                    chunk.ReloadMesh();
            }
        }
    }
}

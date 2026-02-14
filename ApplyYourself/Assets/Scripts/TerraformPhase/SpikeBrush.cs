using System.Collections.Generic;
using UnityEngine;

namespace ApplyYourself
{
    [CreateAssetMenu(fileName = nameof(SpikeBrush), menuName = nameof(SpikeBrush))]
    public class SpikeBrush : Brush
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
                    float dist = Vector3.Distance(Utils.Flatten(vert), Utils.Flatten(point));
                    if (dist > size)
                        continue;

                    vert.y += (size - dist) / size * strength * Time.fixedDeltaTime;
                    chunk.verticies[i] = vert;
                    hasChanged = true;
                }

                if (hasChanged)
                    chunk.ReloadMesh();
            }
        }
    }
}

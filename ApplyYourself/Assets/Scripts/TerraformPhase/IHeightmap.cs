using UnityEngine;

namespace ApplyYourself
{
    public interface IHeightmap
    {
        float GetHeight(float worldX, float worldZ);
    }
}

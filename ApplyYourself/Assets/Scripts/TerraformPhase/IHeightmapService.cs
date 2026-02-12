using UnityEngine;

namespace ApplyYourself
{
    public interface IHeightmapService
    {
        float GetHeight(float worldX, float worldZ);
    }
}

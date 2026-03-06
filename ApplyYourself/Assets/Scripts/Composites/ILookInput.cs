using UnityEngine;

namespace ApplyYourself
{
    public interface ILookInput
    {
        Vector2 GetLook();
        float GetX();
        float GetY();
    }
}
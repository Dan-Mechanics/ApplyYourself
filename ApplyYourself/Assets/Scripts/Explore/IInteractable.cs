using UnityEngine;

namespace ApplyYourself
{
    public interface IInteractable
    {
        Vector3 GetPosition();
        string GetHighlight();
        void Interact();
    }
}

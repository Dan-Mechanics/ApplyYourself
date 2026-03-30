using UnityEngine;

namespace ApplyYourself
{
    public class FollowParent : MonoBehaviour
    {
        private Transform parent;

        private void Start()
        {
            parent = transform.parent;
            transform.SetParent(null);
        }

        private void LateUpdate() => transform.position = parent.position;
    }
}
using UnityEngine;

namespace ApplyYourself
{
    public class ChunkApproximation : MonoBehaviour
    {
        [SerializeField] private MeshRenderer closeRenderer = default;
        [SerializeField] private MeshRenderer farRenderer = default;
        [SerializeField] private float visibleDistance = default;
        [SerializeField] private float approxHeight = default;
        private Transform cam;

        public void Setup(float chunkSize, Transform cam, Vector3 offset)
        {
            this.cam = cam;

            Vector3 scale = Vector3.one * chunkSize;
            scale.y = approxHeight;
            transform.localScale = scale;

            Vector3 pos = offset;
            scale.y = 0f;
            pos += scale * 0.5f;

            transform.position = pos;
            UpdateVisible();
        }

        private void FixedUpdate() => UpdateVisible();

        private void UpdateVisible()
        {
            bool visible = Vector3.Distance(Utils.Flatten(transform.position), Utils.Flatten(cam.position)) <= visibleDistance;
            closeRenderer.enabled = visible;
            farRenderer.enabled = !visible;
        }

        public void UpdateHeight(float height) 
        {
            Vector3 pos = transform.position;
            pos.y = height - approxHeight * 0.5f;
            transform.position = pos;
        }
    }
}

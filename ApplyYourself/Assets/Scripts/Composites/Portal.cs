using UnityEngine;
using UnityEngine.SceneManagement;

namespace ApplyYourself
{
    public class Portal : MonoBehaviour, IInteractable
    {
        [SerializeField] private string scene = default;

        public string GetHighlight() => $"Go through {nameof(Portal)}";
        public Vector3 GetPosition() => transform.position;

        public void Interact() => SceneManager.LoadScene(scene);
    }
}

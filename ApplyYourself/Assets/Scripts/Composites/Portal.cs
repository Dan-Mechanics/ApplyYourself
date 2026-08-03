using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ApplyYourself
{
    public class Portal : MonoBehaviour, IInteractable
    {
        public event Action OnInteract;
        [SerializeField] private string scene = default;

        public string GetHighlight() => $"Go through {nameof(Portal)}";
        public Vector3 GetPosition() => transform.position;
        public void Interact() => OnInteract?.Invoke();
        public void SwitchScenes() => SceneManager.LoadScene(scene);
    }
}

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ApplyYourself
{
    public class Portal : MonoBehaviour, IInteractable
    {
        public event Action OnRequestFadeOut;
        
        [SerializeField] private string scene = default;
        [SerializeField] private bool fadeOut = default;

        public string GetHighlight() => $"Go through {nameof(Portal)}";
        public Vector3 GetPosition() => transform.position;
        public void SetScene(string scene) => this.scene = scene;

        public void Interact()
        {
            if (fadeOut)
            {
                OnRequestFadeOut?.Invoke();
                fadeOut = false;
                return;
            }
            
            SceneManager.LoadScene(scene);
        }
    }
}

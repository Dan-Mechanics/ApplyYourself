using UnityEngine;
using UnityEngine.SceneManagement;

namespace ApplyYourself
{
    public class Portal : MonoBehaviour, IInteractable
    {
        [SerializeField] private Object sandboxScene = default;

        public void Interact()
        {
            SceneManager.LoadScene(sandboxScene.name);
        }
    }
}

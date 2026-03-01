using UnityEngine;
using UnityEngine.SceneManagement;

namespace ApplyYourself
{
    public class Portal : MonoBehaviour, IInteractable
    {
        [SerializeField] private string scene = default;

        public void Interact() => SceneManager.LoadScene(scene);
    }
}

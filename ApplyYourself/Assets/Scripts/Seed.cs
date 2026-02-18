using UnityEngine;
using UnityEngine.SceneManagement;

namespace ApplyYourself
{
    public class Seed : MonoBehaviour
    {
        [SerializeField] private Object placeholderScene = default;
        [SerializeField] private string mainCameraTag = default;    

        private void Start()
        {
            Algorithm algorithm = FindAnyObjectByType<Algorithm>();
            SceneManager.LoadScene(placeholderScene.name, LoadSceneMode.Additive);

            Scene newScene = SceneManager.GetSceneByName(placeholderScene.name);
            Debug.LogWarning(newScene.name);
            GameObject[] placeholders = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();
            Debug.LogWarning(placeholders.Length);
            for (int i = 0; i < placeholders.Length; i++)
            {
                Debug.LogWarning("helloo !!");
                if (placeholders[i].CompareTag(mainCameraTag))
                    continue;

                Instantiate(placeholders[i],
                    placeholders[i].transform.position,
                    placeholders[i].transform.rotation);
            }

            SceneManager.UnloadSceneAsync(placeholderScene.name);

            EndingSetup endingSetup = FindAnyObjectByType<EndingSetup>();
            if (algorithm != null)
            {
                endingSetup.SetAs(algorithm.ending);
                Destroy(algorithm.gameObject);
            }
            else
            {
                endingSetup.ShowPreview();
            }

            Destroy(endingSetup.gameObject);
            Destroy(gameObject);
        }
    }
}

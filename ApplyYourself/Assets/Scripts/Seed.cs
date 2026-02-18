using UnityEngine;
using UnityEngine.SceneManagement;

namespace ApplyYourself
{
    public class Seed : MonoBehaviour
    {
        [SerializeField] private Object placeholderScene = default;

        private void Start()
        {
            Algorithm algorithm = FindAnyObjectByType<Algorithm>();
            //SceneManager.LoadScene(placeholderScene.name, LoadSceneMode.Additive);
            GameObject[] placeholders = SceneManager.GetSceneByName(placeholderScene.name).GetRootGameObjects();
            for (int i = 0; i < placeholders.Length; i++)
            {
                Instantiate(placeholders[i],
                    placeholders[i].transform.position,
                    placeholders[i].transform.rotation).
                    SetActive(placeholders[i].activeSelf);
            }

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

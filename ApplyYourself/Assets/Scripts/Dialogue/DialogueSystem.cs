using UnityEngine;

namespace ApplyYourself
{
    /// <summary>
    /// Goal for this is to make it work well.
    /// </summary>
    public class DialogueSystem : MonoBehaviour
    {
        [SerializeField] private EasyBinding skip = default;

        private void Awake()
        {
            Dialogue[] dialogues = FindObjectsByType<Dialogue>(FindObjectsSortMode.None);
            for (int i = 0; i < dialogues.Length; i++)
            {
                dialogues[i].OnDialogue += ShowDialogue;
            }
        }

        private void ShowDialogue(TextAsset textAsset)
        {
            throw new System.NotImplementedException();
        }
    }
}

using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;

namespace ApplyYourself
{
    [RequireComponent(typeof(TMP_Text))]
    public class TextWriter : MonoBehaviour
    {
        public bool IsDone => isDone;
        private const float INTERVAL = 0.045f;

        [SerializeField] private TMP_Text text = default;
        private readonly StringBuilder builder = new StringBuilder();
        private WaitForSeconds delay;
        private string message;
        private bool isDone;

        public void Write(string message)
        {
            Clear();
            if (!Utils.IsStringValid(message))
                return;

            isDone = false;
            this.message = message;
            gameObject.name = message;

            delay = new WaitForSeconds(INTERVAL);
            StartCoroutine(WriteDelayed());
        }

        private IEnumerator WriteDelayed()
        {
            delay = new WaitForSeconds(INTERVAL);
            for (int i = 0; i < message.Length; i++)
            {
                yield return delay;
                builder.Append(message[i]);
                text.text = builder.ToString();
            }

            text.text = message;
            builder.Clear();
            isDone = true;
        }

        public void Skip()
        {
            Clear();
            text.text = message;
        }

        public void Clear()
        {
            StopAllCoroutines();
            text.text = string.Empty;
            builder.Clear();
            isDone = true;
        }

        private void OnValidate() => text = GetComponent<TMP_Text>();
    }
}

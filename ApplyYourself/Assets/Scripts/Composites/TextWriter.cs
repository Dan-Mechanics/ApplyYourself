using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;

namespace ApplyYourself
{
    [RequireComponent(typeof(TMP_Text))]
    public class TextWriter : MonoBehaviour
    {
        public const float INTERVAL = 0.06f;

        private readonly StringBuilder builder = new StringBuilder();
        private WaitForSeconds delay;
        private TMP_Text text;
        private string message;

        private void Awake()
        {
            text = GetComponent<TMP_Text>();
            delay = new WaitForSeconds(INTERVAL);
        }

        public void Write(string message)
        {
            Clear();
            if (!Utils.IsStringValid(message))
                return;

            this.message = message;
            gameObject.name = message;
            StartCoroutine(WriteDelayed());
        }

        private IEnumerator WriteDelayed()
        {
            for (int i = 0; i < message.Length; i++)
            {
                yield return delay;
                builder.Append(message[i]);
                text.text = builder.ToString();
            }

            text.text = message;
            builder.Clear();
        }

        public void Clear()
        {
            StopAllCoroutines();
            text.text = string.Empty;
            message = string.Empty;
            builder.Clear();
        }

        public void WriteTime(int mins, int secs) => text.text = $"{mins}:{secs}";
    }
}

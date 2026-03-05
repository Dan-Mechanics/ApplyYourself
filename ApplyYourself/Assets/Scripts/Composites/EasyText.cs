using TMPro;
using UnityEngine;

namespace ApplyYourself
{
    [RequireComponent(typeof(TMP_Text))]
    public class EasyText : MonoBehaviour
    {
        [SerializeField] private TMP_Text text = default;

        public void Write(string str) => text.text = str;
        private void OnValidate() => text = GetComponent<TMP_Text>();
    }
}

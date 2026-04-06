using UnityEngine;

namespace ApplyYourself
{
    public class FollowCursor : MonoBehaviour
    {
        [SerializeField] private RectTransform rect = default;

        private void Start()
        {
            Cursor.visible = false;
        }

        private void Update()
        {
            Vector2 cursorPosition = Input.mousePosition;
            cursorPosition.x -= Screen.width / 2f;
            cursorPosition.y -= Screen.height / 2f;

            rect.anchoredPosition = cursorPosition;
        }

        private void OnValidate() => rect = GetComponent<RectTransform>();
    }
}

using UnityEngine;

namespace ApplyYourself
{
    public class SensitivityMouse : MonoBehaviour, ILookInput
    {
        [SerializeField] private EasyBinding next = default;
        [SerializeField] private EasyBinding previous = default;
        [SerializeField] private float sensitivity = default;
        [SerializeField] private float min = default;
        [SerializeField] private float max = default;
        [SerializeField] private float sensitivityPerClick = default;
        [SerializeField] private int decimalPlaces = default;

        [Header("Debug UI")]
        [SerializeField] private Color color = Color.white;
        [SerializeField] private int fontSize = default;
        [SerializeField] private int padding = default;
        [SerializeField] private int width = default;
        [SerializeField] private int height = default;

        private void Update()
        {
            if (next.WasPressed)
                sensitivity = Mathf.Clamp(sensitivity + sensitivityPerClick, min, max);

            if (previous.WasPressed)
                sensitivity = Mathf.Clamp(sensitivity - sensitivityPerClick, min, max);
        }

        public Vector2 GetLook() => new Vector2(-GetY(), GetX());
        public float GetX() => Input.GetAxisRaw("Mouse X") * sensitivity;
        public float GetY() => Input.GetAxisRaw("Mouse Y") * sensitivity;

        private void OnGUI()
        {
            GUI.color = color;
            Rect rect = new Rect(Screen.width - width - padding, Screen.height - height - padding, width, height);
            GUIStyle style = GUI.skin.GetStyle("Label");
            style.fontSize = fontSize;

            style.alignment = TextAnchor.LowerRight;
            GUI.Label(rect, $"sens: {System.Math.Round(sensitivity, decimalPlaces)}\nUse alt mouse buttons to raise / lower", style);
            GUI.color = Color.white;
        }
    }
}

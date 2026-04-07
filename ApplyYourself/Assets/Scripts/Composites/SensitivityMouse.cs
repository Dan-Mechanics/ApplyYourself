using UnityEngine;

namespace ApplyYourself
{
    public class SensitivityMouse : MonoBehaviour, ILookInput
    {
        [SerializeField] private EasyBinding upArrow = default;
        [SerializeField] private EasyBinding downArrow = default;
        [SerializeField] private EasyBinding leftArrow = default;
        [SerializeField] private EasyBinding rightArrow = default;
        [SerializeField] private PersistentFloat sensitivity = default;
        [SerializeField] private PersistentFloat scrollSensitivity = default;
        [SerializeField] private float min = default;
        [SerializeField] private float max = default;
        [SerializeField] private float sensitivityPerClick = default;
        [SerializeField] private int decimalPlaces = default;
        [SerializeField] private bool reloadOnStart = default;

        [Header("Debug UI")]
        [SerializeField] private Color color = Color.white;
        [SerializeField] private int fontSize = default;
        [SerializeField] private int padding = default;
        [SerializeField] private int width = default;
        [SerializeField] private int height = default;

        private void Start()
        {
            if (reloadOnStart)
            {
                sensitivity.value = 1f;
                scrollSensitivity.value = 1f;
            }
        }

        private void Update()
        {
            if (rightArrow.WasPressed)
                sensitivity.value = Mathf.Clamp(sensitivity.value + sensitivityPerClick, min, max);

            if (leftArrow.WasPressed)
                sensitivity.value = Mathf.Clamp(sensitivity.value - sensitivityPerClick, min, max);

            if (upArrow.WasPressed)
                scrollSensitivity.value = Mathf.Clamp(scrollSensitivity.value + sensitivityPerClick, min, max);

            if (downArrow.WasPressed)
                scrollSensitivity.value = Mathf.Clamp(scrollSensitivity.value - sensitivityPerClick, min, max);
        }

        public Vector2 GetLook() => new Vector2(-GetY(), GetX());
        public float GetX() => Input.GetAxisRaw("Mouse X") * sensitivity.value;
        public float GetY() => Input.GetAxisRaw("Mouse Y") * sensitivity.value;
        public float GetScroll() => Input.mouseScrollDelta.y * scrollSensitivity.value;

        private void OnGUI()
        {
            if (!upArrow.IsHeld && !downArrow.IsHeld &&
                !leftArrow.IsHeld && !rightArrow.IsHeld)
                return;

            GUI.color = color;
            Rect rect = new Rect(Screen.width - width - padding, Screen.height - height - padding, width, height);
            GUIStyle style = GUI.skin.GetStyle("Label");
            style.fontSize = fontSize;

            style.alignment = TextAnchor.LowerRight;
            GUI.Label(rect, $"sens: {System.Math.Round(sensitivity.value, decimalPlaces)}\n" +
                $"scroll: {System.Math.Round(scrollSensitivity.value, decimalPlaces)}\n" +
                $"fps: {Mathf.RoundToInt(1f / Time.smoothDeltaTime)}", style);

            GUI.color = Color.white;
        }
    }
}

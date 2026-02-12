using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ApplyYourself
{
    public class BetterButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public event Action OnClick;

        [SerializeField] private KeyCode key = KeyCode.Mouse0;
        [SerializeField] private TMP_Text text = default;
        [SerializeField] private Image image = default;
        [SerializeField] private GameObject blockGraphic = default;

        [SerializeField] private UnityEvent onClick = default;
        [SerializeField] private UnityEvent onSelect = default;
        [SerializeField] private UnityEvent onDeslect = default;

        private bool isSelected;
        private float interactableTime;
        private bool interactable;

        private void Start() => Deselect();

        private void Update()
        {
            interactable = Time.realtimeSinceStartup >= interactableTime;
            if (!CheckClicked())
                return;

            OnClick?.Invoke();
            onClick?.Invoke();
        }

        private void FixedUpdate()
        {
            blockGraphic.SetActive(!interactable);
        }

        private bool CheckClicked()
        {
            return interactable && isSelected &&
                gameObject.activeInHierarchy && Input.GetKeyDown(key);
        }

        public void OnPointerEnter(PointerEventData eventData) => Select();
        public void OnPointerExit(PointerEventData eventData) => Deselect();
        private void OnDisable() => Deselect();

        private void Select()
        {
            if (isSelected)
                return;

            isSelected = true;
            onSelect?.Invoke();
        }

        private void Deselect()
        {
            if (!isSelected)
                return;

            isSelected = false;
            onDeslect?.Invoke();
        }

        public void BlockForSeconds(float value) => interactableTime = Time.time + value;

        public void SetText(string writing) 
        {
            if (text == null)
                return;

            text.text = writing;
        }

        public void SetSprite(Sprite sprite) => image.sprite = sprite;
    }
}
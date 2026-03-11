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
        public event Action<int> OnClickIndex;

        [SerializeField] private EasyBinding primaryFire = default;
        [SerializeField] private TMP_Text text = default;
        [SerializeField] private Image image = default;
        [SerializeField] private GameObject notInteractable = default;

        [SerializeField] private UnityEvent onClick = default;
        [SerializeField] private UnityEvent onSelect = default;
        [SerializeField] private UnityEvent onDeslect = default;

        private bool isSelected;
        private bool interactable;
        private int index;

        public void Setup(int index = 0)
        {
            this.index = index;
            SetInteractable(true);
            Deselect();
        }

        private void OnDisable() => Deselect();

        private void Update()
        {
            if (!HasClicked())
                return;

            OnClickIndex?.Invoke(index);
            OnClick?.Invoke();
            onClick?.Invoke();
        }

        private void FixedUpdate() => notInteractable.SetActive(!interactable);

        private bool HasClicked()
        {
            return interactable && isSelected &&
                gameObject.activeInHierarchy && primaryFire.WasPressed;
        }

        public void OnPointerEnter(PointerEventData eventData) => Select();
        public void OnPointerExit(PointerEventData eventData) => Deselect();

        private void Select()
        {
            if (isSelected || !interactable)
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

        public void SetInteractable(bool interactable) => this.interactable = interactable;
        public void SetText(string str) => text.text = str;
        public void SetSprite(Sprite sprite) => image.sprite = sprite;
    }
}
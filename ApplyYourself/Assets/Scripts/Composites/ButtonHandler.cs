using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace ApplyYourself
{
    public class ButtonHandler : MonoBehaviour
    {
        public event Action<int> OnClick;
        private List<Button> buttons;

        public void Setup()
        {
            buttons = GetComponentsInChildren<Button>().ToList();
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].onClick.AddListener(() => { ClickCallback(i); });
            }
        }

        private void ClickCallback(int index)
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].interactable = i != index;
            }

            OnClick?.Invoke(index);
        }
    }
}

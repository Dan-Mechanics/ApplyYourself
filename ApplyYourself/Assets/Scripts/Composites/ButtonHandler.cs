using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ApplyYourself
{
    public class ButtonHandler : MonoBehaviour
    {
        public event Action<int> OnClick;
        private List<BetterButton> buttons;

        public void Setup()
        {
            buttons = GetComponentsInChildren<BetterButton>().ToList();
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].Setup(i);
                buttons[i].OnClickIndex += ClickCallback;
            }

            buttons[0].SetInteractable(false);
        }

        private void ClickCallback(int index)
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].SetInteractable(i != index);
            }

            OnClick?.Invoke(index);
        }
    }
}

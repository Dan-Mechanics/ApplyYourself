using System;
using UnityEngine;

namespace ApplyYourself
{
    public interface IQuest
    {
        event Action<IQuest> OnQuestFinished;
        event Action<string> OnDisplayString;

        void Setup();
        void FixedUpdate() { }
    }
}

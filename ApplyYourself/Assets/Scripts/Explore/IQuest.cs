using System;
using UnityEngine;

namespace ApplyYourself
{
    public interface IQuest
    {
        public const string LINE = "===============";
        
        event Action<IQuest> OnQuestFinished;
        event Action<string> OnDisplayString;

        void Setup();
        void FixedUpdate() { }
    }
}

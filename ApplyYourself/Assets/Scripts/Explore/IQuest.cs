using System;
using UnityEngine;

namespace ApplyYourself
{
    public interface IQuest
    {
        public const string LINE = "===============";
        
        event Action<IQuest> OnDone;
        event Action<string> OnFeedback;
        void Setup();
        void FixedUpdate() { }
    }
}

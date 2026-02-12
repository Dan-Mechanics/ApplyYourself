using System;

namespace ApplyYourself
{
    public interface IState 
    {
        public event Action<IState> OnYield;
        void Enter();
        void Exit();
        void OnFrame();
        void OnTick();
    }
}

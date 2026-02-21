using UnityEngine;

namespace ApplyYourself
{
    public class Menu : StateBehaviour 
    {
        [SerializeField] private LerpFollow lerpFollow = default;
        [SerializeField] private Transform showMenu = default;
        [SerializeField] private Transform hideMenu = default;

        public void CloseMenu() => YieldState();

        public override void Enter()
        {
            base.Enter();
            lerpFollow.SetTarget(showMenu);
        }

        public override void Exit()
        {
            base.Exit();
            lerpFollow.SetTarget(hideMenu);
        }
    }
}
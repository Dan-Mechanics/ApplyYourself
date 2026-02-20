using System;
using UnityEngine;

namespace ApplyYourself
{
    public class StateBehaviour : MonoBehaviour
    {
        public event Action<StateBehaviour> OnYield;
        public event Action<StateBehaviour> OnClaim;
        public event Action<StateBehaviour> OnDeregister;

        public virtual void Setup() { }
        public virtual void Enter() { print(GetType().ToString().ToLowerInvariant()); }
        public virtual void Exit() { }
        public virtual void OnUpdate() { }
        public virtual void OnFixedUpdate() { }

        protected void YieldState() => OnYield?.Invoke(this);
        protected void ClaimState() => OnClaim?.Invoke(this);
        protected virtual void OnDestroy() => OnDeregister?.Invoke(this);
    }
}

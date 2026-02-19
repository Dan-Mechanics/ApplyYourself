using UnityEngine;

namespace ApplyYourself
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Transform target = default;

        private void Start()
        {
            LerpFollow lerpFollow = GameObject.FindWithTag("MainCamera").GetComponent<LerpFollow>();
            lerpFollow.SetTarget(target);
            lerpFollow.transform.LookAt(transform);
            
        }
    }
}

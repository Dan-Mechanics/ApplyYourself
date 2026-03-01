using System.Globalization;
using System.Threading;
using UnityEngine;

namespace ApplyYourself
{
    public class SceneBoilerplate : MonoBehaviour
    {
        [SerializeField] private int fps = default;
        [SerializeField] private float physicsTicksPerSecond = default;

        private void Start()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Application.targetFrameRate = fps;
            QualitySettings.vSyncCount = 0;
            Time.fixedDeltaTime = 1f / physicsTicksPerSecond;

            Destroy(gameObject);
        }
    }
}
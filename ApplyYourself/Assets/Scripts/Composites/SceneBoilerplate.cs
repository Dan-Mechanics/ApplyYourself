using System.Globalization;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ApplyYourself
{
    public class SceneBoilerplate : MonoBehaviour
    {
        [SerializeField] private EasyBinding reload = default;
        [SerializeField] private EasyBinding escape = default;
        [SerializeField] private EasyBinding shift = default;
        [SerializeField] private EasyBinding ctrl = default;
        [SerializeField] private int fps = default;
        [SerializeField] private float physicsTicksPerSecond = default;

        private void Awake()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Application.targetFrameRate = fps;
            QualitySettings.vSyncCount = 0;
            Time.fixedDeltaTime = 1f / physicsTicksPerSecond;
        }

        private void Update()
        {
            if (reload.WasPressed)
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            if (!shift.IsHeld && !ctrl.IsHeld)
                return;

            if (escape.WasPressed)
                Application.Quit();
        }
    }
}
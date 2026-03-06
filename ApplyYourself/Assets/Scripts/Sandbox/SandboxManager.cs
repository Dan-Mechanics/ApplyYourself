using System.Collections.Generic;
using UnityEngine;

namespace ApplyYourself
{
    /// <summary>
    /// Put the sequence here. Potentially make everyhing
    /// work with heightmap and typemap to keep seperate
    /// </summary>
    public class SandboxManager : MonoBehaviour 
    {
        [SerializeField] private Timer timer = default;
        [SerializeField] private TextWriter timerText = default;
        [SerializeField] private Algorithm algorithm = default;
        [SerializeField] private Terraformer terraformer = default;
        [SerializeField] private PivotController pivotController = default;
        [SerializeField] private WaterManager waterManager = default;
        [SerializeField] private UnitManager unitManager = default;
        [SerializeField] private float interval = default;
        [SerializeField] private List<Brush> brushes = default;
        private readonly FSM fsm = new FSM();

        private void Start()
        {
            timer.OnNewTime += timerText.WriteTime;
            timer.OnDone += algorithm.CompleteSandboxPhase;
            timer.Begin();

            waterManager.Initialize();
            unitManager.Initialize();

            brushes.ForEach(x => x.OnRaise += unitManager.RaiseArea);
            brushes.ForEach(x => x.OnRaise += waterManager.RaiseArea);
            brushes.ForEach(x => x.OnDecorate += unitManager.DecorateArea);
            terraformer.SetBrushes(brushes);

            unitManager.OnNewWaterRange += FindAnyObjectByType<AdaptiveGradient>().SetRange;
            InvokeRepeating(nameof(Tick), interval, interval);
            pivotController.Assign(FindAnyObjectByType<SensitivityMouse>());

            fsm.AddTransition(new StateTransition(terraformer, pivotController));
            fsm.AddTransition(new StateTransition(pivotController, terraformer));
            fsm.AddState(terraformer);
            fsm.AddState(pivotController);

            fsm.Open(terraformer);
        }

        private void Update() => fsm.Update();
        private void FixedUpdate() => fsm.FixedUpdate();

        private void OnDestroy()
        {
            brushes.ForEach(x => x.OnRaise -= unitManager.RaiseArea);
            brushes.ForEach(x => x.OnRaise -= waterManager.RaiseArea);
            brushes.ForEach(x => x.OnDecorate -= unitManager.DecorateArea);
        }

        private void Tick()
        {
            waterManager.Tick();
            unitManager.RenderAll();
        }
    }
}
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
        [SerializeField] private Bridge algorithm = default;
        [SerializeField] private Terraformer terraformer = default;
        [SerializeField] private PivotController pivotController = default;
        [SerializeField] private WaterManager waterManager = default;
        [SerializeField] private LandManager landManager = default;
        [SerializeField] private UnitManager unitManager = default;
        [SerializeField] private TextureHeightmap heightmap = default;
        [SerializeField] private TextureTypemap typemap = default;
        [SerializeField] private float tickInterval = default;
        [SerializeField] private List<Brush> brushes = default;

        private readonly FSM fsm = new FSM();
        private float next;

        private void Start()
        {
            timer.OnNewTime += timerText.WriteTime;
            timer.OnDone += algorithm.CompleteSandboxPhase;
            timer.Begin();

            landManager.Initialize(heightmap, typemap, waterManager);
            waterManager.Initialize(landManager, landManager);
            unitManager.Initialize(landManager, landManager, waterManager);

            foreach (Brush brush in brushes)
            {
                brush.OnRaise += waterManager.RaiseArea;
                brush.OnRaise += landManager.RaiseArea;
                brush.OnDecorate += landManager.DecorateArea;
            }

            landManager.OnAreaUpdated += unitManager.RenderArea;
            landManager.OnRedecorate += unitManager.RedecorateArea;

            terraformer.SetBrushes(brushes);
            unitManager.OnNewWaterRange += FindAnyObjectByType<AdaptiveGradient>().SetRange;
            pivotController.Assign(FindAnyObjectByType<SensitivityMouse>());

            fsm.AddTransition(new StateTransition(terraformer, pivotController));
            fsm.AddTransition(new StateTransition(pivotController, terraformer));
            fsm.AddState(terraformer);
            fsm.AddState(pivotController);

            fsm.Open(terraformer);
        }

        private void Update() => fsm.Update();
        private void FixedUpdate()
        {
            fsm.FixedUpdate();

            if (Time.time < next)
                return;

            next = Time.time + tickInterval;
            Tick();
        }

        private void OnDestroy()
        {
            foreach (Brush brush in brushes)
            {
                brush.OnRaise -= waterManager.RaiseArea;
                brush.OnRaise -= landManager.RaiseArea;
                brush.OnDecorate -= landManager.DecorateArea;
            }
        }

        private void Tick()
        {
            waterManager.Tick();
            unitManager.RenderAll();
        }
    }
}
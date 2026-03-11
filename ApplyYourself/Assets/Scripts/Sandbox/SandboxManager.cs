using System.Collections.Generic;
using UnityEngine;

namespace ApplyYourself
{
    public class SandboxManager : MonoBehaviour 
    {
        [SerializeField] private TextWriter timerText = default;
        [SerializeField] private Terraformer terraformer = default;
        [SerializeField] private PivotController pivotController = default;
        [SerializeField] private WaterManager waterManager = default;
        [SerializeField] private LandManager landManager = default;
        [SerializeField] private UnitManager unitManager = default;
        [SerializeField] private float interval = default;
        [SerializeField] private List<Brush> brushes = default;

        private readonly FSM fsm = new FSM();
        private float next;
        private Timer timer;
        private TextureHeightmap heightmapStartup;
        private TextureTypemap typemapStartup;
        private Algorithm algorithm;
        private Bridge bridge;

        private void Awake()
        {
            timer = FindAnyObjectByType<Timer>();
            algorithm = FindAnyObjectByType<Algorithm>();
            bridge = FindAnyObjectByType<Bridge>();
            heightmapStartup = FindAnyObjectByType<TextureHeightmap>();
            typemapStartup = FindAnyObjectByType<TextureTypemap>();
        }

        private void Start()
        {
            timer.OnNewTime += timerText.WriteTime;
            timer.OnDone += bridge.GoNextPhase;
            timer.Begin();

            landManager.Setup(heightmapStartup, typemapStartup, waterManager.Heightmap);
            waterManager.Setup(landManager.Heightmap, landManager.Typemap);
            unitManager.Setup(landManager.Typemap, landManager.Heightmap, waterManager.Heightmap);

            foreach (Brush brush in brushes)
            {
                brush.OnRaise += waterManager.RaiseArea;
                brush.OnRaise += landManager.RaiseArea;
                brush.OnDecorate += landManager.DecorateArea;
            }

            landManager.OnRaiseArea += unitManager.RenderArea;
            landManager.OnDecorateArea += unitManager.RenderAreaDecoration;

            terraformer.SetBrushes(brushes);
            unitManager.OnNewWaterRange += FindAnyObjectByType<AdaptiveGradient>().SetRange;
            pivotController.Assign(FindAnyObjectByType<SensitivityMouse>());

            algorithm.Setup(landManager.Heightmap, waterManager.Heightmap, landManager.Typemap);

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

            next = Time.time + interval;
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
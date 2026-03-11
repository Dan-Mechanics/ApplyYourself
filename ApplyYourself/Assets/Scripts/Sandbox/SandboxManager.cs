using System.Collections.Generic;
using UnityEngine;

namespace ApplyYourself
{
    public class SandboxManager : MonoBehaviour 
    {
        [SerializeField] private float interval = default;
        [SerializeField] private List<Brush> brushes = default;

        private readonly FSM fsm = new FSM();
        private float nextTickTime;

        private AdaptiveGradient adaptiveGradient;
        private SensitivityMouse sensitivityMouse;
        private EasyText easyText;
        private Terraformer terraformer;
        private PivotController pivotController;
        private WaterManager waterManager;
        private LandManager landManager;
        private UnitManager unitManager;
        private ButtonHandler buttonHandler;
        private TextureHeightmap heightmapStartup;
        private TextureTypemap typemapStartup;
        private LerpFollow lerpFollow;
        private Algorithm algorithm;
        private Portal portal;
        private Bridge bridge;
        private Timer timer;
        private Camera cam;

        private void Awake()
        {
            cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();

            lerpFollow = FindAnyObjectByType<LerpFollow>();
            portal = FindAnyObjectByType<Portal>();
            easyText = FindAnyObjectByType<EasyText>();
            terraformer = FindAnyObjectByType<Terraformer>();
            pivotController = FindAnyObjectByType<PivotController>();
            waterManager = FindAnyObjectByType<WaterManager>();
            landManager = FindAnyObjectByType<LandManager>();
            unitManager = FindAnyObjectByType<UnitManager>();
            adaptiveGradient = FindAnyObjectByType<AdaptiveGradient>();
            sensitivityMouse = FindAnyObjectByType<SensitivityMouse>();

            timer = FindAnyObjectByType<Timer>();
            algorithm = FindAnyObjectByType<Algorithm>();
            bridge = FindAnyObjectByType<Bridge>();
            heightmapStartup = FindAnyObjectByType<TextureHeightmap>();
            typemapStartup = FindAnyObjectByType<TextureTypemap>();
            buttonHandler = FindAnyObjectByType<ButtonHandler>();
        }

        private void Start()
        {
            bridge.Setup(algorithm, portal);
            timer.OnNewTime += easyText.WriteTime;
            timer.OnDone += bridge.GoNextPhase;
            timer.Begin();

            // ===

            waterManager.Initialize();
            landManager.Initialize(heightmapStartup, typemapStartup);

            waterManager.Setup(landManager.Heightmap, landManager.Typemap);
            landManager.Setup(waterManager.Heightmap);

            unitManager.Setup(landManager.Typemap, landManager.Heightmap, waterManager.Heightmap);

            // ===

            foreach (Brush brush in brushes)
            {
                brush.OnRaise += waterManager.RaiseArea;
                brush.OnRaise += landManager.RaiseArea;
                brush.OnDecorate += landManager.DecorateArea;
            }

            landManager.OnRaiseArea += unitManager.RenderArea;
            landManager.OnDecorateArea += unitManager.RenderAreaDecoration;

            terraformer.Setup(brushes, cam);

            buttonHandler.Setup();
            buttonHandler.OnClick += terraformer.SelectBrush;
            
            unitManager.OnNewWaterRange += adaptiveGradient.SetRange;
            pivotController.Setup(sensitivityMouse);

            algorithm.Setup(landManager.Heightmap, waterManager.Heightmap, landManager.Typemap);

            lerpFollow.SetTarget(pivotController.transform.GetChild(0));
            lerpFollow.SetLookTarget(pivotController.transform);

            // ===

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
            if (Time.time < nextTickTime)
                return;

            nextTickTime = Time.time + interval;
            waterManager.Tick();
            unitManager.RenderAll();
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
    }
}
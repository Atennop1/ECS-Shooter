using Leopotam.EcsLite;
using Sirenix.OdinInspector;

namespace Shooter.EntryPoint
{
    public sealed class Game : SerializedMonoBehaviour
    {
        private EcsWorld _ecsWorld;
        private IEcsSystems _ecsSystems;
        private IEcsSystems _fixedEcsSystems;
        
        private void Awake()
        {
            _ecsWorld = new EcsWorld();
            _ecsSystems = new EcsSystems(_ecsWorld);
            _fixedEcsSystems = new EcsSystems(_ecsWorld);

            _ecsSystems.Init();
            _fixedEcsSystems.Init();
        }

        private void Update()
            => _ecsSystems.Run();

        private void FixedUpdate()
            => _fixedEcsSystems.Run();

        private void OnDestroy()
        {
            _ecsSystems.Destroy();
            _fixedEcsSystems.Destroy();
            _ecsWorld.Destroy();
        }
    }
}
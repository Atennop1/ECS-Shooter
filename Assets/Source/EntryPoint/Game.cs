using Leopotam.EcsLite;
using Shooter.Character;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.EntryPoint
{
    public sealed class Game : SerializedMonoBehaviour
    {
        [SerializeField] private CharacterFactory _characterFactory;
        
        private EcsWorld _ecsWorld;
        private IEcsSystems _ecsSystems;
        private IEcsSystems _fixedEcsSystems;
        
        private void Awake()
        {
            _ecsWorld = new EcsWorld();
            _ecsSystems = new EcsSystems(_ecsWorld);
            _fixedEcsSystems = new EcsSystems(_ecsWorld);

            _characterFactory.Create(_ecsSystems);

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
using Leopotam.EcsLite;
using Shooter.Input;

namespace Shooter.Character
{
    public sealed class CharacterSprintingSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly float _sprintingSpeed;
        private float _walkingSpeed;

        public CharacterSprintingSystem(float sprintingSpeed) 
            => _sprintingSpeed = sprintingSpeed;

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var pool = world.GetPool<CharacterMoving>();
            var filter = world.Filter<CharacterMoving>().End();

            foreach (var entity in filter) 
                _walkingSpeed = pool.Get(entity).Speed;
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            
            var characterMovingPool = world.GetPool<CharacterMoving>();
            var playerInputPool = world.GetPool<PlayerInput>();
            
            var playerInputFilter = world.Filter<PlayerInput>().End();
            var characterMovingFilter = world.Filter<CharacterMoving>().End();

            foreach (var playerInputEntity in playerInputFilter)
            {
                foreach (var characterMovementEntity in characterMovingFilter)
                {
                    var playerInput = playerInputPool.Get(playerInputEntity);
                    ref var characterMoving = ref characterMovingPool.Get(characterMovementEntity);
                    characterMoving.Speed = playerInput.IsShiftPressed ? _sprintingSpeed : _walkingSpeed;
                }
            }
        }
    }
}
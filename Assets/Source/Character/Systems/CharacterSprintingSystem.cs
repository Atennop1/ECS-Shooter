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
            var pool = world.GetPool<Character>();
            var filter = world.Filter<Character>().End();

            foreach (var entity in filter) 
                _walkingSpeed = pool.Get(entity).MovingData.Speed;
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            
            var characterPool = world.GetPool<Character>();
            var characterFilter = world.Filter<Character>().End();
            
            var playerInputPool = world.GetPool<PlayerInput>();
            var playerInputFilter = world.Filter<PlayerInput>().End();

            foreach (var playerInputEntity in playerInputFilter)
            {
                foreach (var characterMovementEntity in characterFilter)
                {
                    var playerInput = playerInputPool.Get(playerInputEntity);
                    ref var character = ref characterPool.Get(characterMovementEntity);
                    character.MovingData.Speed = playerInput.IsShiftPressed ? _sprintingSpeed : _walkingSpeed;
                }
            }
        }
    }
}
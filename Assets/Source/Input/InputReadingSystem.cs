using System.Numerics;
using Leopotam.EcsLite;

namespace Shooter.Input
{
    public sealed class InputReadingSystem : IEcsInitSystem
    {
        private readonly CharacterControls _characterControls;

        public InputReadingSystem(CharacterControls characterControls) 
            => _characterControls = characterControls;

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<InputData>().End();
            var pool = world.GetPool<InputData>();

            foreach (var entity in filter)
            {
                _characterControls.CharacterMovement.Movement.performed += context =>
                {
                    var changedDirection = context.ReadValue<Vector2>();
                    pool.Get(entity).MovingDirection.x = changedDirection.X;
                    pool.Get(entity).MovingDirection.y = changedDirection.Y;
                };
            }
        }
    }
}
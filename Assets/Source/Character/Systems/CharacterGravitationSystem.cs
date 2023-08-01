using Leopotam.EcsLite;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterGravitationSystem : IEcsRunSystem
    {
        private readonly CharacterController _characterController;

        public CharacterGravitationSystem(CharacterController characterController)
            => _characterController = characterController;
        
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var pool = world.GetPool<CharacterMovement>();
            var filter = world.Filter<CharacterMovement>().End();

            foreach (var entity in filter)
            {
                ref var movement = ref pool.Get(entity);

                if (movement is { IsGrounded: true, Velocity: { y: < 0 } }) 
                    movement.Velocity.y = -2f;

                movement.Velocity.y += movement.GravitationalConstant * Time.deltaTime;
                _characterController.Move(movement.Velocity * Time.deltaTime);
            }
        }
    }
}
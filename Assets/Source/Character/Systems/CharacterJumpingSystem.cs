using Leopotam.EcsLite;
using Shooter.Input;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterJumpingSystem : IEcsRunSystem
    {
        private readonly CharacterController _characterController;

        public CharacterJumpingSystem(CharacterController characterController)
            => _characterController = characterController;

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var characterMovementPool = world.GetPool<CharacterMovement>();
            var playerInputPool = world.GetPool<PlayerInput>();
            var filter = world.Filter<CharacterMovement>().Inc<PlayerInput>().End();

            foreach (var entity in filter)
            {
                ref var movement = ref characterMovementPool.Get(entity);
                ref var input = ref playerInputPool.Get(entity);

                if (!movement.IsGrounded || !input.IsJumpKeyPressed) 
                    continue;
                
                movement.Velocity.y = Mathf.Sqrt(-2 * movement.JumpHeight * movement.GravitationalConstant);
                _characterController.Move(movement.Velocity * Time.deltaTime);
            }
        }
    }
}
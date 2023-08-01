using Leopotam.EcsLite;
using Shooter.Input;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterMovingSystem : IEcsRunSystem
    {
        private readonly CharacterController _characterController;

        public CharacterMovingSystem(CharacterController characterController) 
        => _characterController = characterController;

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var characterMovementPool = world.GetPool<CharacterMovement>();
            var playerInputPool = world.GetPool<PlayerInput>();
            var filter = world.Filter<CharacterMovement>().Inc<PlayerInput>().End();

            foreach (var entity in filter)
            {
                var movementInput = playerInputPool.Get(entity).MovementInput;
                var characterMovementSpeed = characterMovementPool.Get(entity).Speed;
                
                var movementDirection = _characterController.transform.right * movementInput.x + _characterController.transform.forward * movementInput.y;
                _characterController.Move(movementDirection * characterMovementSpeed * Time.deltaTime);
            }
        }
    }
}
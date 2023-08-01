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
            
            var characterMovingPool = world.GetPool<CharacterMoving>();
            var playerInputPool = world.GetPool<PlayerInput>();
            
            var playerInputFilter = world.Filter<PlayerInput>().End();
            var characterMovingFilter = world.Filter<CharacterMoving>().End();

            foreach (var playerInputEntity in playerInputFilter)
            {
                foreach (var characterMovementEntity in characterMovingFilter)
                {
                    var movementInput = playerInputPool.Get(playerInputEntity).MovementInput;
                    var characterMovementSpeed = characterMovingPool.Get(characterMovementEntity).Speed;

                    var movementDirection = _characterController.transform.right * movementInput.x + _characterController.transform.forward * movementInput.y;
                    _characterController.Move(movementDirection * characterMovementSpeed * Time.deltaTime);
                }
            }
        }
    }
}
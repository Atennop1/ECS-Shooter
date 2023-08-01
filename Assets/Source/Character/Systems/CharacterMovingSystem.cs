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
                    var playerInput = playerInputPool.Get(playerInputEntity);
                    var characterMoving = characterMovingPool.Get(characterMovementEntity);

                    var movementDirection = _characterController.transform.right * playerInput.MovementInput.x + _characterController.transform.forward * playerInput.MovementInput.y;
                    _characterController.Move(movementDirection * characterMoving.Speed * Time.deltaTime);
                }
            }
        }
    }
}
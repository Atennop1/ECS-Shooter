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
            
            var movingPool = world.GetPool<CharacterMoving>();
            var movingFilter = world.Filter<CharacterMoving>().End();
            
            var playerInputPool = world.GetPool<PlayerInput>();
            var playerInputFilter = world.Filter<PlayerInput>().End();

            foreach (var playerInputEntity in playerInputFilter)
            {
                foreach (var characterMovementEntity in movingFilter)
                {
                    var playerInput = playerInputPool.Get(playerInputEntity);
                    var moving = movingPool.Get(characterMovementEntity);

                    var addedVelocity = _characterController.transform.right * playerInput.MovementInput.x + _characterController.transform.forward * playerInput.MovementInput.y;
                    moving.IsWalking = addedVelocity != Vector3.zero;
                    _characterController.Move(addedVelocity * moving.Speed * Time.deltaTime);
                }
            }
        }
    }
}
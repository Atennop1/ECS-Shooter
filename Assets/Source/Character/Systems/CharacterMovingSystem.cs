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
            
            var characterPool = world.GetPool<Character>();
            var characterFilter = world.Filter<Character>().End();
            
            var playerInputPool = world.GetPool<PlayerInput>();
            var playerInputFilter = world.Filter<PlayerInput>().End();

            foreach (var playerInputEntity in playerInputFilter)
            {
                foreach (var characterMovementEntity in characterFilter)
                {
                    var playerInput = playerInputPool.Get(playerInputEntity);
                    var character = characterPool.Get(characterMovementEntity);

                    var movementDirection = _characterController.transform.right * playerInput.MovementInput.x + _characterController.transform.forward * playerInput.MovementInput.y;
                    _characterController.Move(movementDirection * character.MovingData.Speed * Time.deltaTime);
                }
            }
        }
    }
}
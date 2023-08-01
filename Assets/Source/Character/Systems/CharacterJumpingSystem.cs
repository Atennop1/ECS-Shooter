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
            
            var characterPool = world.GetPool<Character>();
            var characterFilter = world.Filter<Character>().End();
            
            var playerInputPool = world.GetPool<PlayerInput>();
            var playerInputFilter = world.Filter<PlayerInput>().End();

            foreach (var playerInputEntity in playerInputFilter)
            {
                foreach (var characterMovementEntity in characterFilter)
                {
                    ref var character = ref characterPool.Get(characterMovementEntity);
                    ref var input = ref playerInputPool.Get(playerInputEntity);

                    if (!character.IsGrounded || !input.IsJumpKeyPressed) 
                        continue;
                
                    character.Jumping.VerticalVelocity = Mathf.Sqrt(-2 * character.Jumping.JumpHeight * character.Jumping.GravitationalConstant);
                    _characterController.Move(new Vector3(0, character.Jumping.VerticalVelocity, 0) * Time.deltaTime);
                }
            }
        }
    }
}
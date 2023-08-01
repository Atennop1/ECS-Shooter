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
            
            var characterJumpingPool = world.GetPool<CharacterJumping>();
            var playerInputPool = world.GetPool<PlayerInput>();
            
            var playerInputFilter = world.Filter<PlayerInput>().End();
            var characterJumpingFilter = world.Filter<CharacterJumping>().End();

            foreach (var playerInputEntity in playerInputFilter)
            {
                foreach (var characterMovementEntity in characterJumpingFilter)
                {
                    ref var jumping = ref characterJumpingPool.Get(characterMovementEntity);
                    ref var input = ref playerInputPool.Get(playerInputEntity);

                    if (!jumping.IsGrounded || !input.IsJumpKeyPressed) 
                        continue;
                
                    jumping.VerticalVelocity = Mathf.Sqrt(-2 * jumping.JumpHeight * jumping.GravitationalConstant);
                    _characterController.Move(new Vector3(0, jumping.VerticalVelocity, 0) * Time.deltaTime);
                }
            }
        }
    }
}
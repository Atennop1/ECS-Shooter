using Leopotam.EcsLite;
using Shooter.Input;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterJumpingSystem : IEcsRunSystem
    {
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

                    if (!character.IsGrounded || !input.IsJumpKeyPressed || character.MovingData.IsSliding) 
                        continue;
                
                    character.JumpingData.VerticalVelocity = Mathf.Sqrt(-2 * character.JumpingData.JumpHeight * character.JumpingData.GravitationalConstant);
                }
            }
        }
    }
}
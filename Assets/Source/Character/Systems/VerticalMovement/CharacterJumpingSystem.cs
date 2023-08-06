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
            
            var jumpingPool = world.GetPool<CharacterJumping>();
            var slidingPool = world.GetPool<CharacterSliding>();
            var componentsFilter = world.Filter<CharacterJumping>().Inc<CharacterSliding>().End();
            
            var playerInputPool = world.GetPool<PlayerInput>();
            var playerInputFilter = world.Filter<PlayerInput>().End();

            foreach (var playerInputEntity in playerInputFilter)
            {
                foreach (var characterComponentsEntity in componentsFilter)
                {
                    ref var jumping = ref jumpingPool.Get(characterComponentsEntity);
                    ref var input = ref playerInputPool.Get(playerInputEntity);

                    if (!jumping.IsGrounded || !input.IsJumpKeyPressed || slidingPool.Get(characterComponentsEntity).IsActive) 
                        continue;
                
                    jumping.VerticalVelocity = Mathf.Sqrt(-2 * jumping.JumpHeight * jumping.GravitationalConstant);
                }
            }
        }
    }
}
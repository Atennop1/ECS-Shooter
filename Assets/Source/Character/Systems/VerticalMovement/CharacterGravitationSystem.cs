using Leopotam.EcsLite;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterGravitationSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var pool = world.GetPool<CharacterJumping>();
            var filter = world.Filter<CharacterJumping>().End();

            foreach (var entity in filter)
            {
                ref var jumping = ref pool.Get(entity);
                
                if (!jumping.IsGrounded)
                    jumping.VerticalVelocity += jumping.GravitationalConstant * Time.deltaTime;
            }
        }
    }
}
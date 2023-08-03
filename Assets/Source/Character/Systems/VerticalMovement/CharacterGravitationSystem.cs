using Leopotam.EcsLite;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterGravitationSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var pool = world.GetPool<Character>();
            var filter = world.Filter<Character>().End();

            foreach (var entity in filter)
            {
                ref var character = ref pool.Get(entity);

                if (character.IsGrounded && character.JumpingData.VerticalVelocity < 0) 
                    character.JumpingData.VerticalVelocity = -2f;

                character.JumpingData.VerticalVelocity += character.JumpingData.GravitationalConstant * Time.deltaTime;
            }
        }
    }
}
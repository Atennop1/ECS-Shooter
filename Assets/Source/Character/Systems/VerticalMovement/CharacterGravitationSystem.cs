using Leopotam.EcsLite;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterGravitationSystem : IEcsRunSystem
    {
        private readonly CharacterController _characterController;

        public CharacterGravitationSystem(CharacterController characterController)
            => _characterController = characterController;
        
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var pool = world.GetPool<Character>();
            var filter = world.Filter<Character>().End();

            foreach (var entity in filter)
            {
                ref var character = ref pool.Get(entity);

                if (character.IsGrounded && character.MovingData.Velocity.y < 0) 
                    character.MovingData.Velocity.y = -2f;

                character.MovingData.Velocity.y += character.JumpingData.GravitationalConstant * Time.deltaTime;
            }
        }
    }
}
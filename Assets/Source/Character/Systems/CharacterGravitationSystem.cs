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
            var pool = world.GetPool<CharacterJumping>();
            var filter = world.Filter<CharacterJumping>().End();

            foreach (var entity in filter)
            {
                ref var jumping = ref pool.Get(entity);

                if (jumping is { IsGrounded: true, VerticalVelocity: < 0 }) 
                    jumping.VerticalVelocity = -2f;

                jumping.VerticalVelocity += jumping.GravitationalConstant * Time.deltaTime;
                _characterController.Move(new Vector3(0, jumping.VerticalVelocity, 0) * Time.deltaTime);
            }
        }
    }
}
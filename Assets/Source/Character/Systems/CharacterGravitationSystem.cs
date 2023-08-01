using System;
using Leopotam.EcsLite;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterGravitationSystem : IEcsRunSystem
    {
        private readonly CharacterController _characterController;
        private Vector3 _velocity;

        private const float _gravitationalConstant = -9.81f;

        public CharacterGravitationSystem(CharacterController characterController) 
            => _characterController = characterController ?? throw new ArgumentNullException(nameof(characterController));

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var pool = world.GetPool<CharacterMovement>();
            var filter = world.Filter<CharacterMovement>().End();

            foreach (var entity in filter)
            {
                if (pool.Get(entity).IsGrounded && _velocity.y < 0)
                {
                    _velocity.y = -2f;
                    continue;
                }

                _velocity.y += _gravitationalConstant * Time.deltaTime;
                _characterController.Move(_velocity * Time.deltaTime);
            }
        }
    }
}
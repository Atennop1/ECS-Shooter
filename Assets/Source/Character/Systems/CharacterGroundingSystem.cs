using System;
using Leopotam.EcsLite;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterGroundingSystem : IEcsRunSystem
    {
        private readonly Transform _characterFeetTransform;
        private readonly LayerMask _groundLayerMask;
        
        private const float _checkingSphereRadius = 0.4f;

        public CharacterGroundingSystem(Transform characterFeetTransform, LayerMask groundLayerMask)
        {
            _characterFeetTransform = characterFeetTransform ?? throw new ArgumentNullException(nameof(characterFeetTransform));
            _groundLayerMask = groundLayerMask;
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var pool = world.GetPool<CharacterJumping>();
            var filter = world.Filter<CharacterJumping>().End();

            foreach (var entity in filter)
                pool.Get(entity).IsGrounded = Physics.CheckSphere(_characterFeetTransform.position, _checkingSphereRadius, _groundLayerMask);
        }
    }
}
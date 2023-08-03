using System;
using Leopotam.EcsLite;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterSlopingSystem : IEcsRunSystem
    {
        private readonly CharacterController _characterController;
        
        public CharacterSlopingSystem(CharacterController characterController) 
            => _characterController = characterController ?? throw new ArgumentNullException(nameof(characterController));

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var pool = world.GetPool<Character>();
            var filter = world.Filter<Character>().End();

            foreach (var entity in filter)
            {
                ref var character = ref pool.Get(entity);

                if (!character.IsGrounded || !Physics.Raycast(_characterController.transform.position, Vector3.down, out var raycastHit, 4f)) 
                    continue;
                
                var hitPointNormal = raycastHit.normal;

                if (!(Vector3.Angle(hitPointNormal, Vector3.up) > _characterController.slopeLimit))
                {
                    character.MovingData.IsSliding = false;
                    continue;
                }
                
                character.MovingData.IsSliding = true;
                _characterController.Move(new Vector3(hitPointNormal.x, -hitPointNormal.y, hitPointNormal.z) * character.MovingData.SlopSpeed * Time.deltaTime);
            }
        }
    }
}
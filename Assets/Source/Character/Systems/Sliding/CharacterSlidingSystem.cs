using System;
using Leopotam.EcsLite;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterSlidingSystem : IEcsRunSystem
    {
        private readonly CharacterController _characterController;
        private readonly LayerMask _layerMask = LayerMask.GetMask($"Ground");
        
        public CharacterSlidingSystem(CharacterController characterController) 
            => _characterController = characterController ?? throw new ArgumentNullException(nameof(characterController));

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var pool = world.GetPool<Character>();
            var filter = world.Filter<Character>().End();

            foreach (var entity in filter)
            {
                ref var character = ref pool.Get(entity);

                if (!character.IsGrounded) 
                    continue;
                
                //Debug.Log("Is grounded");
                if (!character.SlidingData.IsSliding)
                    continue;

                //Debug.Log("Sliding");
                var normal = character.SlidingData.SlidingSurfaceNormal;
                _characterController.Move(new Vector3(normal.x, -normal.y, normal.z) * character.MovingData.SlopSpeed * Time.deltaTime);
            }
        }
    }
}
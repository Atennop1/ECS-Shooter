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
            var filter = world.Filter<CharacterSliding>().Inc<CharacterJumping>().End();
            
            var slidingPool = world.GetPool<CharacterSliding>();
            var jumpingPool = world.GetPool<CharacterJumping>();

            foreach (var entity in filter)
            {
                ref var sliding = ref slidingPool.Get(entity);
                ref var jumping = ref jumpingPool.Get(entity);

                if (!jumping.IsGrounded) 
                    continue;
                
                //Debug.Log("Is grounded");
                if (!sliding.IsActive)
                    continue;

                //Debug.Log("Sliding");
                var normal = sliding.SlidingSurfaceNormal;
                _characterController.Move(new Vector3(normal.x, -normal.y, normal.z) * sliding.SlideSpeed * Time.deltaTime);
            }
        }
    }
}
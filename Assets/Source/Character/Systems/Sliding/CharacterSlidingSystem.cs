using System;
using Scellecs.Morpeh;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterSlidingSystem : ISystem
    {
        private readonly CharacterController _characterController;
        private Entity _characterEntity;
        
        public CharacterSlidingSystem(CharacterController characterController) 
            => _characterController = characterController ?? throw new ArgumentNullException(nameof(characterController));

        public World World { get; set; }
        
        public void OnAwake()
        {
            var filter = World.Filter.With<CharacterHeadMovingComponent>().With<CharacterMovingComponent>().With<CharacterJumpingComponent>();
            _characterEntity = filter.FirstOrDefault();
        }

        public void OnUpdate(float deltaTime)
        {
            if (_characterEntity == null)
                return;
            
            ref var sliding = ref _characterEntity.GetComponent<CharacterSlidingComponent>();
            ref var grounded = ref _characterEntity.GetComponent<CharacterGroundedComponent>();

            if (!grounded.IsActive || !sliding.IsActive)
                return;
            
            var normal = sliding.SlidingSurfaceNormal;
            _characterController.Move(new Vector3(normal.x, -normal.y, normal.z) * sliding.SlideSpeed * Time.deltaTime);
        }

        public void Dispose() { }
    }
}
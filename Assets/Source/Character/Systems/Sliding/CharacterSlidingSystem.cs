using System;
using Scellecs.Morpeh;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterSlidingSystem : ISystem
    {
        private readonly CharacterController _characterController;
        
        public CharacterSlidingSystem(CharacterController characterController) 
            => _characterController = characterController ?? throw new ArgumentNullException(nameof(characterController));

        public World World { get; set; }

        public void OnUpdate(float deltaTime)
        {
            var filter = World.Filter.With<CharacterSlidingComponent>().With<CharacterGroundedComponent>();
            var entity = filter.FirstOrDefault();

            if (entity == null)
                return;

            ref var sliding = ref entity.GetComponent<CharacterSlidingComponent>();
            ref var grounded = ref entity.GetComponent<CharacterGroundedComponent>();

            if (!grounded.IsActive || !sliding.IsActive)
                return;
            
            var normal = sliding.SlidingSurfaceNormal;
            _characterController.Move(new Vector3(normal.x, -normal.y, normal.z) * sliding.SlideSpeed * Time.deltaTime);
        }

        public void Dispose() { }
        public void OnAwake()  { }
    }
}
using System;
using Scellecs.Morpeh;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterVerticalVelocityApplyingSystem : ISystem
    {
        private readonly CharacterController _characterController;
        private Entity _characterEntity;

        public CharacterVerticalVelocityApplyingSystem(CharacterController characterController)
            => _characterController = characterController;

        public World World { get; set; }
        
        public void OnAwake()
        {
            var filter = World.Filter.With<CharacterJumpingComponent>();
            _characterEntity = filter.FirstOrDefault();
            
            if (_characterEntity == null)
                throw new InvalidOperationException("This system can't work without character on scene");
        }
        
        public void OnUpdate(float deltaTime)
        {
            ref var jumping = ref _characterEntity.GetComponent<CharacterJumpingComponent>(); 
            _characterController.Move(new Vector3(0, jumping.VerticalVelocity, 0) * Time.deltaTime);
        }

        public void Dispose() { }
    }
}
﻿using Scellecs.Morpeh;

namespace Shooter.Character
{
    public sealed class CharacterCrouchingApplyingSystem : ISystem
    {
        private Entity _characterEntity;
        private readonly float _crouchingSpeed;

        public CharacterCrouchingApplyingSystem(float crouchingSpeed) 
            => _crouchingSpeed = crouchingSpeed;

        public World World { get; set; }

        public void OnAwake()
        {
            var characterFilter = World.Filter.With<CharacterMovingComponent>().With<CharacterCrouchingComponent>();
            _characterEntity = characterFilter.FirstOrDefault();
        }
        
        public void OnUpdate(float deltaTime)
        {
            if (_characterEntity == null)
                return;

            ref var crouching = ref _characterEntity.GetComponent<CharacterCrouchingComponent>();
            ref var moving = ref _characterEntity.GetComponent<CharacterMovingComponent>();
            ref var sprinting = ref _characterEntity.GetComponent<CharacterSprintingComponent>();

            sprinting.CanSprint = sprinting.CanSprint && !crouching.IsActive;
            
            if (crouching.IsActive)
                moving.Speed = _crouchingSpeed;
        }
        
        public void Dispose() { }
    }
}
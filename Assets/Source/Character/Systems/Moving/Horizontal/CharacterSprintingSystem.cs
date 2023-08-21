﻿using Scellecs.Morpeh;
using Shooter.Input;

namespace Shooter.Character
{
    public sealed class CharacterSprintingSystem : ISystem
    {
        private readonly float _sprintingSpeed;
        private float _walkingSpeed;
        
        private Entity _characterEntity;
        private Entity _inputEntity;

        public CharacterSprintingSystem(float sprintingSpeed)
            => _sprintingSpeed = sprintingSpeed;

        public World World { get; set; }

        public void OnAwake()
        {
            var characterFilter = World.Filter.With<CharacterMovingComponent>();
            var movementInputFilter = World.Filter.With<MovementInputComponent>();

            _characterEntity = characterFilter.FirstOrDefault();
            _inputEntity = movementInputFilter.FirstOrDefault();
            
            if (_characterEntity != null)
                _walkingSpeed = _characterEntity.GetComponent<CharacterMovingComponent>().Speed;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_characterEntity == null || _inputEntity == null)
                return;
            
            ref var input = ref _inputEntity.GetComponent<MovementInputComponent>();
            ref var moving = ref _characterEntity.GetComponent<CharacterMovingComponent>();
            ref var sprinting = ref _characterEntity.GetComponent<CharacterSprintingComponent>();
            ref var crouching = ref _characterEntity.GetComponent<CharacterCrouchingComponent>();

            sprinting.IsActive = false;
            
            if (crouching.IsActive || crouching.IsTransiting)
                return;
            
            moving.Speed = input.IsSprintKeyPressed && sprinting.CanSprint ? _sprintingSpeed : _walkingSpeed;
            sprinting.IsActive = input.IsSprintKeyPressed && sprinting.CanSprint && moving.IsWalking;

            if (moving.IsWalking)
                moving.IsWalking = !sprinting.IsActive;
        }
        
        public void Dispose()  { }
    }
}
using System;
using Scellecs.Morpeh;
using Shooter.Input;
using Shooter.Tools;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterCrouchingSystem : ISystem
    {
        private readonly CharacterController _characterController;
        private readonly CharacterCrouchingStateSwitcher _characterCrouchingStateSwitcher;
        private readonly float _crouchingSpeed;
        
        private Entity _characterEntity;
        private Entity _inputEntity;

        public CharacterCrouchingSystem(CharacterController characterController, CharacterCrouchingStateSwitcher characterCrouchingStateSwitcher, float crouchingSpeed)
        {
            _characterController = characterController ?? throw new ArgumentNullException(nameof(characterController));
            _characterCrouchingStateSwitcher = characterCrouchingStateSwitcher ?? throw new ArgumentNullException(nameof(characterCrouchingStateSwitcher));
            _crouchingSpeed = crouchingSpeed.ThrowExceptionIfLessOrEqualsZero();
        }

        public World World { get; set; }
        
        public void OnAwake()
        {
            var characterFilter = World.Filter.With<CharacterCrouchingComponent>().With<CharacterGroundedComponent>().With<CharacterSprintingComponent>();
            var inputFilter = World.Filter.With<CrouchingInputComponent>();
            
            _characterEntity = characterFilter.FirstOrDefault();
            _inputEntity = inputFilter.FirstOrDefault();
        }
        
        public async void OnUpdate(float deltaTime)
        {
            if (_characterEntity == null || _inputEntity == null)
                return;

            var crouchingInput = _inputEntity.GetComponent<CrouchingInputComponent>();
            var crouching = _characterEntity.GetComponent<CharacterCrouchingComponent>();
            var grounded = _characterEntity.GetComponent<CharacterGroundedComponent>();

            var checkingRaycastPosition = _characterController.transform.position + new Vector3(0, _characterController.height / 2, 0);
            var nothingPrevents = !crouching.IsActive || !UnityEngine.Physics.Raycast(checkingRaycastPosition, Vector3.up, 1.8f);
            
            if (!crouching.IsTransiting && grounded.IsActive && crouchingInput.IsCrouchKeyPressedThisFrame && nothingPrevents)
                await _characterCrouchingStateSwitcher.SwitchFor(_characterEntity);

            if (crouching.IsActive) 
                _characterEntity.GetComponent<CharacterMovingComponent>().CurrentSpeed = _crouchingSpeed;
        }

        public void Dispose() { }
    }
}
using System;
using Scellecs.Morpeh;
using Shooter.Input;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterCrouchingSystem : ISystem
    {
        private readonly CharacterController _characterController;
        private readonly CharacterCrouchingStateSwitcher _characterCrouchingStateSwitcher;
        
        private Entity _characterEntity;
        private Entity _inputEntity;

        public CharacterCrouchingSystem(CharacterController characterController, CharacterCrouchingStateSwitcher characterCrouchingStateSwitcher)
        {
            _characterController = characterController ?? throw new ArgumentNullException(nameof(characterController));
            _characterCrouchingStateSwitcher = characterCrouchingStateSwitcher ?? throw new ArgumentNullException(nameof(characterCrouchingStateSwitcher));
        }

        public World World { get; set; }
        
        public void OnAwake()
        {
            var characterFilter = World.Filter.With<CharacterCrouchingComponent>().With<CharacterGroundedComponent>();
            var inputFilter = World.Filter.With<CrouchingInputComponent>();
            
            _characterEntity = characterFilter.FirstOrDefault();
            _inputEntity = inputFilter.FirstOrDefault();
        }
        
        public async void OnUpdate(float deltaTime)
        {
            if (_characterEntity == null || _inputEntity == null)
                return;

            var crouching = _characterEntity.GetComponent<CharacterCrouchingComponent>();
            var grounded = _characterEntity.GetComponent<CharacterGroundedComponent>();
            var crouchingInput = _inputEntity.GetComponent<CrouchingInputComponent>();
            
            var checkingRaycastPosition = _characterController.transform.position + new Vector3(0, _characterController.height / 2 - _characterController.radius, 0);
            if (crouching.IsTransiting || !grounded.IsActive || !crouchingInput.IsCrouchKeyPressedNow || (crouching.IsActive && UnityEngine.Physics.Raycast(checkingRaycastPosition, Vector3.up, 1f)))
                return;

            await _characterCrouchingStateSwitcher.SwitchFor(_characterEntity);
        }

        public void Dispose() { }
    }
}
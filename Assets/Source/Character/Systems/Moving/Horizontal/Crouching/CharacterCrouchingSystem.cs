using System;
using Cysharp.Threading.Tasks;
using Scellecs.Morpeh;
using Shooter.Input;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterCrouchingSystem : ISystem
    {
        private readonly CharacterController _characterController;
        private readonly float _timeToCrouch;
        private bool _isChanging;
        
        private Entity _characterEntity;
        private Entity _inputEntity;

        public CharacterCrouchingSystem(CharacterController characterController, float timeToCrouch)
        {
            _characterController = characterController ?? throw new ArgumentNullException(nameof(characterController));
            _timeToCrouch = timeToCrouch;
        }

        public World World { get; set; }
        
        public void OnAwake()
        {
            var characterFilter = World.Filter.With<CharacterCrouchingComponent>();
            var inputFilter = World.Filter.With<CrouchingInputComponent>();
            
            _characterEntity = characterFilter.FirstOrDefault();
            _inputEntity = inputFilter.FirstOrDefault();
        }
        
        public async void OnUpdate(float deltaTime)
        {
            if (_characterEntity == null || _inputEntity == null)
                return;

            var checkingRaycastPosition = _characterController.transform.position + new Vector3(0, _characterController.height / 2 - _characterController.radius, 0);
            if (_isChanging || UnityEngine.Physics.Raycast(checkingRaycastPosition, Vector3.up, 1f) || !_inputEntity.GetComponent<CrouchingInputComponent>().IsCrouchKeyPressedNow)
                return;

            await ChangeState();
        }

        public void Dispose() { }

        private async UniTask ChangeState()
        {
            var crouching = _characterEntity.GetComponent<CharacterCrouchingComponent>();
            
            var elapsedTime = 0f;
            _isChanging = true;

            var targetHeight = crouching.IsActive ? crouching.StandingStateData.Height : crouching.CrouchingStateData.Height;
            var currentHeight = _characterController.height;
            var targetCenter = crouching.IsActive ? crouching.StandingStateData.Center : crouching.CrouchingStateData.Center;
            var currentCenter = _characterController.center;

            while (elapsedTime < _timeToCrouch)
            {
                _characterController.height = Mathf.Lerp(currentHeight, targetHeight, elapsedTime / _timeToCrouch);
                _characterController.center = Vector3.Lerp(currentCenter, targetCenter, elapsedTime / _timeToCrouch);

                elapsedTime += Time.deltaTime;
                await UniTask.Yield();
            }

            _characterController.height = targetHeight;
            _characterController.center = targetCenter;

            crouching.IsActive = !crouching.IsActive;
            _isChanging = false;
        }
    }
}
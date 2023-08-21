using System;
using Cysharp.Threading.Tasks;
using Scellecs.Morpeh;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterCrouchingStateSwitcher
    {
        private readonly CharacterController _characterController;
        private readonly float _timeToCrouch;

        public CharacterCrouchingStateSwitcher(CharacterController characterController, float timeToCrouch)
        {
            _characterController = characterController ?? throw new ArgumentNullException(nameof(characterController));
            _timeToCrouch = timeToCrouch;
        }
        
        public async UniTask SwitchFor(Entity characterEntity)
        {
            characterEntity.GetComponent<CharacterCrouchingComponent>().IsTransiting = true;
            var crouching = characterEntity.GetComponent<CharacterCrouchingComponent>();
            var elapsedTime = 0f;

            var targetHeight = crouching.IsActive ? crouching.StandingStateData.Height : crouching.CrouchingStateData.Height;
            var currentHeight = _characterController.height;
            
            var targetCenter = crouching.IsActive ? crouching.StandingStateData.Center : crouching.CrouchingStateData.Center;
            var currentCenter = _characterController.center;

            while (elapsedTime < _timeToCrouch)
            {
                _characterController.center = Vector3.Lerp(currentCenter, targetCenter, elapsedTime / _timeToCrouch);
                _characterController.height = Mathf.Lerp(currentHeight, targetHeight, elapsedTime / _timeToCrouch);

                elapsedTime += Time.deltaTime;
                await UniTask.Yield();
            }

            _characterController.height = targetHeight;
            _characterController.center = targetCenter;

            characterEntity.GetComponent<CharacterCrouchingComponent>().IsActive = !characterEntity.GetComponent<CharacterCrouchingComponent>().IsActive;
            characterEntity.GetComponent<CharacterCrouchingComponent>().IsTransiting = false;
        }
    }
}
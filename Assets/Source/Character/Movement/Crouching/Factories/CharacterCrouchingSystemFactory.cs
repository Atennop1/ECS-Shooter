using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterCrouchingSystemFactory : SerializedMonoBehaviour
    {
        [SerializeField] private float _timeToCrouch;
        [SerializeField] private float _crouchingSpeed;
        [SerializeField] private CharacterController _characterController;

        public CharacterCrouchingSystem Create()
        {
            var stateSwitcher = new CharacterCrouchingStateSwitcher(_characterController, _timeToCrouch);
            return new CharacterCrouchingSystem(_characterController, stateSwitcher, _crouchingSpeed);
        }
    }
}
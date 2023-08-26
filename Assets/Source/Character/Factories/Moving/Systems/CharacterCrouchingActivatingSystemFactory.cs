using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterCrouchingActivatingSystemFactory : SerializedMonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private float _timeToCrouch;

        public CharacterCrouchingActivatingSystem Create()
        {
            var stateSwitcher = new CharacterCrouchingStateSwitcher(_characterController, _timeToCrouch);
            return new CharacterCrouchingActivatingSystem(_characterController, stateSwitcher);
        }
    }
}
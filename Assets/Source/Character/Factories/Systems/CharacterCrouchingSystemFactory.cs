using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterCrouchingSystemFactory : SerializedMonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private float _timeToCrouch;

        public CharacterCrouchingSystem Create()
        {
            var stateSwitcher = new CharacterCrouchingStateSwitcher(_characterController, _timeToCrouch);
            return new CharacterCrouchingSystem(_characterController, stateSwitcher);
        }
    }
}
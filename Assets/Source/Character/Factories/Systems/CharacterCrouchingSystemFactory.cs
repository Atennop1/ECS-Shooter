using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterCrouchingSystemFactory : SerializedMonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private float _timeToCrouch;

        public CharacterCrouchingSystem Create()
            => new(_characterController, _timeToCrouch);
    }
}
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterCrouchingSpeedSetSystemFactory : SerializedMonoBehaviour
    {
        [SerializeField] private float _crouchingSpeed;

        public CharacterCrouchingSpeedSetSystem Create()
            => new(_crouchingSpeed);
    }
}
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterCrouchingApplyingSystemFactory : SerializedMonoBehaviour
    {
        [SerializeField] private float _crouchingSpeed;

        public CharacterCrouchingApplyingSystem Create()
            => new(_crouchingSpeed);
    }
}
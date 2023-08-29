using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterMovingActivatingSystemFactory : SerializedMonoBehaviour
    {
        [SerializeField] private float _speed;
        
        public CharacterMovingActivatingSystem Create()
            => new(_speed);
    }
}
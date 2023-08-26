using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterFootstepsPlayingSystemFactory : SerializedMonoBehaviour
    {
        [SerializeField] private Transform _characterTransform;
        [SerializeField] private AudioSource _footstepsAudioSource;

        public CharacterFootstepsPlayingSystem Create()
            => new(_characterTransform, _footstepsAudioSource);
    }
}
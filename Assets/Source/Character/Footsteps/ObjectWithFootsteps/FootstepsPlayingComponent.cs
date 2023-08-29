using UnityEngine;

namespace Shooter.Character
{
    public struct FootstepsPlayingComponent : IFootstepsPlayingComponent
    {
        [field: SerializeField] public AudioClip[] FootstepsClips { get; private set; }
    }
}
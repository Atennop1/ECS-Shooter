using System;
using UnityEngine;

namespace Shooter.Character
{
    [Serializable]
    public struct FootstepsPlayingComponent : IFootstepsPlayingComponent
    {
        [field: SerializeField] public AudioClip[] FootstepsClips { get; private set; }
    }
}
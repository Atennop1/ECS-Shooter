using System;
using Scellecs.Morpeh;
using UnityEngine;

namespace Shooter.Character
{
    [Serializable]
    public struct FootstepsPlayingComponent : IComponent
    {
        [field: SerializeField] public AudioClip[] FootstepsClips { get; private set; }
    }
}
using Scellecs.Morpeh;
using UnityEngine;

namespace Shooter.Character
{
    public interface IFootstepsPlayingComponent : IComponent
    {
        public AudioClip[] FootstepsClips { get; }
    }
}
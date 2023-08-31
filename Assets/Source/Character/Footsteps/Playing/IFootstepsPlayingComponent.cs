using Scellecs.Morpeh;
using UnityEngine;

namespace Shooter.Character
{
    public interface IFootstepsPlayingComponent : IComponent
    { 
        AudioClip[] FootstepsClips { get; }
    }
}
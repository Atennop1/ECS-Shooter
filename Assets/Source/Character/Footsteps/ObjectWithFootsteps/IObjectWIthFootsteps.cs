using UnityEngine;

namespace Shooter.Character
{
    public interface IObjectWithFootsteps
    {
        public AudioClip[] FootstepsClips { get; }
    }
}
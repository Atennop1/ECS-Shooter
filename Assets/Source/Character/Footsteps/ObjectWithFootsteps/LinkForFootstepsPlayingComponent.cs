using UnityEngine;

namespace Shooter.Character
{
    public struct LinkForFootstepsPlayingComponent : IFootstepsPlayingComponent
    {
        public IFootstepsPlayingComponent FootstepsPlayingComponent;

        public AudioClip[] FootstepsClips 
            => FootstepsPlayingComponent.FootstepsClips;
    }
}
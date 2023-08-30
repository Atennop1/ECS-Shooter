using UnityEngine;

namespace Shooter.Character
{
    public struct LinkForFootstepsPlayingComponent : IFootstepsPlayingComponent
    {
        public FootstepsPlayingProvider FootstepsPlayingProvider;

        public AudioClip[] FootstepsClips 
            => FootstepsPlayingProvider.GetComponent<IFootstepsPlayingComponent>().FootstepsClips;
    }
}
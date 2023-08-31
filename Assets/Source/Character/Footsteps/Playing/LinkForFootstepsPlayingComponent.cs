using System;
using UnityEngine;

namespace Shooter.Character
{
    [Serializable]
    public struct LinkForFootstepsPlayingComponent : IFootstepsPlayingComponent
    {
        public FootstepsPlayingProvider FootstepsPlayingProvider;

        public AudioClip[] FootstepsClips 
            => FootstepsPlayingProvider.GetComponent<IFootstepsPlayingComponent>().FootstepsClips;
    }
}
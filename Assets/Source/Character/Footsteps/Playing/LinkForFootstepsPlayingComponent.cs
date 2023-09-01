using System;
using Scellecs.Morpeh;
using UnityEngine;

namespace Shooter.Character
{
    [Serializable]
    public struct LinkForFootstepsPlayingComponent : IComponent
    {
        public FootstepsPlayingProvider FootstepsPlayingProvider;

        public AudioClip[] FootstepsClips 
            => FootstepsPlayingProvider.Entity.GetComponent<FootstepsPlayingComponent>().FootstepsClips;
    }
}
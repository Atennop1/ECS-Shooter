using System;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Shooter.Character
{
    [Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct LinkForFootstepsSoundComponent : IComponent
    {
        public FootstepsSound _footstepsSound;

        public AudioClip[] FootstepsClips 
            => _footstepsSound.Entity.GetComponent<FootstepsSoundComponent>().Clips;
    }
}
﻿using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Shooter.Character
{
    [System.Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct CharacterCrouchingComponent : IComponent
    {
        public bool IsActive;
        public bool IsTransiting;
        
        public CharacterCrouchingStateData StandingStateData;
        public CharacterCrouchingStateData CrouchingStateData;
    }
}
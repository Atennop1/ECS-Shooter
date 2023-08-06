using Scellecs.Morpeh;
using UnityEngine.Serialization;

namespace Shooter.Character
{
    public struct CharacterGroundedComponent : IComponent
    {
        [FormerlySerializedAs("IsGrounded")] public bool IsActive;
    }
}
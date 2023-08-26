using Scellecs.Morpeh;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterCrouchingComponentFactory : SerializedMonoBehaviour
    {
        [SerializeField] private CharacterCrouchingStateData _standingStateData;
        [SerializeField] private CharacterCrouchingStateData _crouchingStateData;

        public void CreateFor(Entity entity)
        {
            ref var createdComponent = ref entity.AddComponent<CharacterCrouchingComponent>();

            createdComponent.StandingStateData = _standingStateData;
            createdComponent.CrouchingStateData = _crouchingStateData;
        }
    }
}
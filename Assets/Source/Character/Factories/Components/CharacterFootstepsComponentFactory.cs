using Scellecs.Morpeh;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterFootstepsComponentFactory : SerializedMonoBehaviour
    {
        [SerializeField] private float _crouchingStepTime;
        [SerializeField] private float _walkingStepTime;
        [SerializeField] private float _sprintingStepTime;

        public void CreateFor(Entity entity)
        {
            ref var createdComponent = ref entity.AddComponent<CharacterFootstepsComponent>();

            createdComponent.CrouchingStepTime = _crouchingStepTime;
            createdComponent.WalkingStepTime = _walkingStepTime;
            createdComponent.SprintingStepTime = _sprintingStepTime;
        }
    }
}
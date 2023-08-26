using Scellecs.Morpeh;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterHeadBobComponentFactory : SerializedMonoBehaviour
    {
        [SerializeField] private CharacterHeadbobData _crouchingData;
        [SerializeField] private CharacterHeadbobData _walkingBobData;
        [SerializeField] private CharacterHeadbobData _sprintingBobData;

        public void CreateFor(Entity entity)
        {
            ref var createdComponent = ref entity.AddComponent<CharacterHeadBobComponent>();

            createdComponent.CrouchingBobData = _crouchingData;
            createdComponent.WalkingBobData = _walkingBobData;
            createdComponent.SprintingBobData = _sprintingBobData;
        }
    }
}
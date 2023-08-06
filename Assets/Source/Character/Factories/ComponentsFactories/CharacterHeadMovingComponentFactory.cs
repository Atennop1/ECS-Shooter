using Scellecs.Morpeh;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterHeadMovingComponentFactory : MonoBehaviour
    {
        [SerializeField] private float _mouseSensitivity;
        
        [Space]
        [SerializeField] private CharacterHeadbobData _walkingBobData;
        [SerializeField] private CharacterHeadbobData _sprintingBobData;

        public void CreateFor(Entity entity)
        {
            ref var createdComponent = ref entity.AddComponent<CharacterHeadMovingComponent>();
            
            createdComponent.MouseSensitivity = _mouseSensitivity; 
            createdComponent.WalkingBobData = _walkingBobData;
            createdComponent.SprintingBobData = _sprintingBobData;
        }
    }
}
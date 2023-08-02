using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterHeadMovingDataFactory : MonoBehaviour
    {
        [SerializeField] private float _mouseSensitivity;
        
        [Space]
        [SerializeField] private CharacterMovingModeBobData _walkingBobData;
        [SerializeField] private CharacterMovingModeBobData _sprintingBobData;

        public CharacterHeadMovingData Create()
        {
            var createdMoving = new CharacterHeadMovingData
            {
                MouseSensitivity = _mouseSensitivity,
                WalkingBobData = _walkingBobData,
                SprintingBobData = _sprintingBobData
            };
            
            return createdMoving;
        }
    }
}
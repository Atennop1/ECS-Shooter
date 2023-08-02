using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterHeadMovingDataFactory : MonoBehaviour
    {
        [SerializeField] private float _mouseSensitivity;

        public CharacterHeadMovingData Create()
        {
            var createdMoving = new CharacterHeadMovingData
            {
                MouseSensitivity = _mouseSensitivity
            };
            
            return createdMoving;
        }
    }
}
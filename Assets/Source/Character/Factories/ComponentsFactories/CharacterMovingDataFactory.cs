using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterMovingDataFactory : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _slopSpeed;

        public CharacterMovingData Create()
        {
            var createdMoving = new CharacterMovingData
            {
                Speed = _speed,
                SlopSpeed = _slopSpeed
            };

            return createdMoving;
        }
    }
}
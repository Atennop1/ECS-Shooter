using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterMovingDataFactory : MonoBehaviour
    {
        [SerializeField] private float _speed;

        public CharacterMovingData Create()
        {
            var createdMoving = new CharacterMovingData
            {
                Speed = _speed
            };

            return createdMoving;
        }
    }
}
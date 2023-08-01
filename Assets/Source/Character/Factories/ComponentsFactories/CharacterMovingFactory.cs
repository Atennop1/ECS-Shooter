using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterMovingFactory : MonoBehaviour
    {
        [SerializeField] private float _speed;

        public CharacterMoving Create()
        {
            var createdMoving = new CharacterMoving
            {
                Speed = _speed
            };

            return createdMoving;
        }
    }
}
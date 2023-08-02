using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterJumpingDataFactory : MonoBehaviour
    {
        [SerializeField] private float _jumpHeight;
        [SerializeField] private float _gravitationalConstant;

        public CharacterJumpingData Create()
        {
            var createdJumping = new CharacterJumpingData
            {
                JumpHeight = _jumpHeight,
                GravitationalConstant = _gravitationalConstant
            };

            return createdJumping;
        }
    }
}
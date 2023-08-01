using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterJumpingFactory : MonoBehaviour
    {
        [SerializeField] private float _jumpHeight;
        [SerializeField] private float _gravitationalConstant;

        public CharacterJumping Create()
        {
            var createdJumping = new CharacterJumping
            {
                JumpHeight = _jumpHeight,
                GravitationalConstant = _gravitationalConstant
            };

            return createdJumping;
        }
    }
}
using UnityEngine;

namespace Shooter.Character
{
    public struct CharacterMovement
    {
        public bool IsGrounded;
        public Vector3 Velocity;
        
        public float JumpHeight;
        public float GravitationalConstant;
        
        public float Speed;
        public float MouseSensitivity;
    }
}
namespace Shooter.Character
{
    public struct Character
    {
        public bool IsGrounded;

        public CharacterMoving Moving;
        public CharacterJumping Jumping;
        public CharacterCameraMoving CameraMoving;
    }
}
namespace Shooter.Character
{
    public struct Character
    {
        public bool IsGrounded;

        public CharacterMovingData MovingData;
        public CharacterSlidingData SlidingData;
        public CharacterJumpingData JumpingData;
        public CharacterHeadMovingData HeadMovingData;
    }
}
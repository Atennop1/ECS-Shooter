using Scellecs.Morpeh;

namespace Shooter.Character
{
    public sealed class CharacterCrouchingSpeedSettingSystem : ISystem
    {
        private readonly float _crouchingSpeed;
        private Entity _characterEntity;

        public CharacterCrouchingSpeedSettingSystem(float crouchingSpeed) 
            => _crouchingSpeed = crouchingSpeed;

        public World World { get; set; }

        public void OnAwake()
        {
            var characterFilter = World.Filter.With<CharacterMovingComponent>().With<CharacterCrouchingComponent>();
            _characterEntity = characterFilter.FirstOrDefault();
        }
        
        public void OnUpdate(float deltaTime)
        {
            if (_characterEntity == null)
                return;

            var crouching = _characterEntity.GetComponent<CharacterCrouchingComponent>();
            var moving = _characterEntity.GetComponent<CharacterMovingComponent>();

            if (crouching.IsActive)
                moving.Speed = _crouchingSpeed;
        }
        
        public void Dispose() { }
    }
}
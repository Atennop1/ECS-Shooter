using Scellecs.Morpeh;

namespace Shooter.Character
{
    public sealed class CharacterCrouchingSpeedSetSystem : ISystem
    {
        private readonly float _crouchingSpeed;
        private Entity _characterEntity;

        public CharacterCrouchingSpeedSetSystem(float crouchingSpeed) 
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

            ref var crouching = ref _characterEntity.GetComponent<CharacterCrouchingComponent>();
            ref var moving = ref _characterEntity.GetComponent<CharacterMovingComponent>();

            if (crouching.IsActive)
                moving.Speed = _crouchingSpeed;
        }
        
        public void Dispose() { }
    }
}
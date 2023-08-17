using Scellecs.Morpeh;

namespace Shooter.Character
{
    public sealed class CharacterCrouchingSpeedSetSystem : ISystem
    {
        private readonly float _crouchingSpeed;
        private float _walkingSpeed;
        
        private Entity _characterEntity;

        public CharacterCrouchingSpeedSetSystem(float crouchingSpeed) 
            => _crouchingSpeed = crouchingSpeed;

        public World World { get; set; }

        public void OnAwake()
        {
            var characterFilter = World.Filter.With<CharacterMovingComponent>().With<CharacterCrouchingComponent>();
            _characterEntity = characterFilter.FirstOrDefault();
            
            if (_characterEntity != null)
                _walkingSpeed = _characterEntity.GetComponent<CharacterMovingComponent>().Speed;
        }
        
        public void OnUpdate(float deltaTime)
        {
            if (_characterEntity == null)
                return;

            ref var crouching = ref _characterEntity.GetComponent<CharacterCrouchingComponent>();
            ref var moving = ref _characterEntity.GetComponent<CharacterMovingComponent>();

            if (!moving.IsWalking) 
                return;
            
            moving.Speed = crouching.IsActive ? _crouchingSpeed : _walkingSpeed;
            moving.IsWalking = !crouching.IsActive;
        }
        
        public void Dispose() { }
    }
}
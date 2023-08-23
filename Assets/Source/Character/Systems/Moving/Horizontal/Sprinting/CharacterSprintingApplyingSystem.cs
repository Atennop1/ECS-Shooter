using Scellecs.Morpeh;

namespace Shooter.Character
{
    public sealed class CharacterSprintingApplyingSystem : ISystem
    {
        private Entity _characterEntity;

        private readonly float _sprintingSpeed;
        private float _walkingSpeed;
        
        public CharacterSprintingApplyingSystem(float sprintingSpeed)
            => _sprintingSpeed = sprintingSpeed;

        public World World { get; set; }

        public void OnAwake()
        {
            var characterFilter = World.Filter.With<CharacterMovingComponent>();
            _characterEntity = characterFilter.FirstOrDefault();
            
            if (_characterEntity != null)
                _walkingSpeed = _characterEntity.GetComponent<CharacterMovingComponent>().Speed;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_characterEntity == null)
                return;
            
            ref var moving = ref _characterEntity.GetComponent<CharacterMovingComponent>();
            ref var sprinting = ref _characterEntity.GetComponent<CharacterSprintingComponent>();
            
            moving.Speed = sprinting.IsActive ? _sprintingSpeed : _walkingSpeed;

            if (moving.IsWalking)
                moving.IsWalking = !sprinting.IsActive;
        }
        
        public void Dispose()  { }
    }
}
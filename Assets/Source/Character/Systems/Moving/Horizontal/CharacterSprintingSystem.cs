using Scellecs.Morpeh;
using Shooter.Input;

namespace Shooter.Character
{
    public sealed class CharacterSprintingSystem : ISystem
    {
        private readonly float _sprintingSpeed;
        private float _walkingSpeed;
        
        private Entity _characterEntity;
        private Entity _inputEntity;

        public CharacterSprintingSystem(float sprintingSpeed)
            => _sprintingSpeed = sprintingSpeed;

        public World World { get; set; }

        public void OnAwake()
        {
            var characterFilter = World.Filter.With<CharacterMovingComponent>();
            var movementInputFilter = World.Filter.With<MovementInputComponent>();

            _characterEntity = characterFilter.FirstOrDefault();
            _inputEntity = movementInputFilter.FirstOrDefault();
            
            if (_characterEntity != null)
                _walkingSpeed = _characterEntity.GetComponent<CharacterMovingComponent>().Speed;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_characterEntity == null || _inputEntity == null)
                return;
            
            ref var moving = ref _characterEntity.GetComponent<CharacterMovingComponent>();
            ref var input = ref _inputEntity.GetComponent<MovementInputComponent>();

            moving.Speed = input.IsSprintPressed ? _sprintingSpeed : _walkingSpeed;
            moving.IsSprinting = input.IsSprintPressed && moving.IsWalking;

            if (moving.IsWalking)
                moving.IsWalking = !input.IsSprintPressed;
        }
        
        public void Dispose()  { }
    }
}
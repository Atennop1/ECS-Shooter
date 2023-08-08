using System;
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
            var playerInputFilter = World.Filter.With<PlayerInputComponent>();

            _characterEntity = characterFilter.FirstOrDefault();
            _inputEntity = playerInputFilter.FirstOrDefault();
            
            if (_characterEntity == null || _inputEntity == null)
                throw new InvalidOperationException("This system can't work without character or input on scene");
            
            _walkingSpeed = _characterEntity.GetComponent<CharacterMovingComponent>().Speed;
        }

        public void OnUpdate(float deltaTime)
        {
            ref var moving = ref _characterEntity.GetComponent<CharacterMovingComponent>();
            ref var input = ref _inputEntity.GetComponent<PlayerInputComponent>();

            moving.Speed = input.IsShiftPressed ? _sprintingSpeed : _walkingSpeed;
            moving.IsSprinting = input.IsShiftPressed;

            if (moving.IsWalking)
                moving.IsWalking = !input.IsShiftPressed;
        }
        
        public void Dispose()  { }
    }
}
using Scellecs.Morpeh;
using Shooter.Input;

namespace Shooter.Character
{
    public sealed class CharacterSprintingSystem : ISystem
    {
        private readonly float _sprintingSpeed;
        private float _walkingSpeed;

        public CharacterSprintingSystem(float sprintingSpeed)
            => _sprintingSpeed = sprintingSpeed;

        public World World { get; set; }

        public void OnAwake()
        {
            var filter = World.Filter.With<CharacterMovingComponent>();
            var movingEntity = filter.FirstOrDefault();

            if (movingEntity != null)
                _walkingSpeed = movingEntity.GetComponent<CharacterMovingComponent>().Speed;
        }

        public void OnUpdate(float deltaTime)
        {
            var movingFilter = World.Filter.With<CharacterMovingComponent>();
            var inputFilter = World.Filter.With<PlayerInputComponent>();

            var movingEntity = movingFilter.FirstOrDefault();
            var inputEntity = inputFilter.FirstOrDefault();

            if (movingEntity == null || inputEntity == null)
                return;

            ref var moving = ref movingEntity.GetComponent<CharacterMovingComponent>();
            ref var input = ref inputEntity.GetComponent<PlayerInputComponent>();

            moving.Speed = input.IsShiftPressed ? _sprintingSpeed : _walkingSpeed;
            moving.IsSprinting = input.IsShiftPressed;

            if (moving.IsWalking)
                moving.IsWalking = !input.IsShiftPressed;
        }
        
        public void Dispose()  { }
    }
}
using Scellecs.Morpeh;
using Shooter.Tools;

namespace Shooter.Character
{
    public sealed class CharacterSprintingApplyingSystem : ISystem
    {
        private Entity _characterEntity;
        private readonly float _sprintingSpeed;
        
        public CharacterSprintingApplyingSystem(float sprintingSpeed)
            => _sprintingSpeed = sprintingSpeed.ThrowExceptionIfLessOrEqualsZero();

        public World World { get; set; }

        public void OnAwake()
        {
            var characterFilter = World.Filter.With<CharacterMovingComponent>();
            _characterEntity = characterFilter.FirstOrDefault();
        }

        public void OnUpdate(float deltaTime)
        {
            if (_characterEntity == null)
                return;
            
            ref var moving = ref _characterEntity.GetComponent<CharacterMovingComponent>();
            ref var sprinting = ref _characterEntity.GetComponent<CharacterSprintingComponent>();
            
            if (!sprinting.IsActive)
                return;
            
            moving.CurrentSpeed = _sprintingSpeed; 
            moving.IsWalking = false;
        }
        
        public void Dispose()  { }
    }
}
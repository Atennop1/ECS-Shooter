using Scellecs.Morpeh;
using Shooter.Input;
using Shooter.Tools;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterSprintingSystem : ISystem
    {
        private readonly float _sprintingSpeed;
        
        private Entity _characterEntity;
        private Entity _inputEntity;

        public CharacterSprintingSystem(float sprintingSpeed)
            => _sprintingSpeed = sprintingSpeed.ThrowExceptionIfLessOrEqualsZero();

        public World World { get; set; }

        public void OnAwake()
        {
            var characterFilter = World.Filter.With<CharacterSprintingComponent>().With<CharacterGroundedComponent>().With<CharacterMovingComponent>();
            var sprintingInputFilter = World.Filter.With<SprintingInputComponent>();

            _characterEntity = characterFilter.FirstOrDefault();
            _inputEntity = sprintingInputFilter.FirstOrDefault();
        }

        public void OnUpdate(float deltaTime)
        {
            if (_characterEntity == null || _inputEntity == null)
                return;
            
            ref var sprinting = ref _characterEntity.GetComponent<CharacterSprintingComponent>();
            ref var grounded = ref _characterEntity.GetComponent<CharacterGroundedComponent>();
            ref var moving = ref _characterEntity.GetComponent<CharacterMovingComponent>();
            ref var input = ref _inputEntity.GetComponent<SprintingInputComponent>();

            if (input.IsSprintKeyPressed && sprinting.CanSprint && grounded.IsActive && moving.IsWalking)
                sprinting.IsActive = true;

            if (input.IsSprintedKeyReleasedThisFrame || !sprinting.CanSprint || !moving.IsWalking) 
                sprinting.IsActive = false;
            
            if (!sprinting.IsActive)
                return;
            
            moving.CurrentSpeed = _sprintingSpeed; 
            moving.IsWalking = false;
        }
        
        public void Dispose()  { }
    }
}
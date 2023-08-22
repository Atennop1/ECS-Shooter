using Scellecs.Morpeh;
using Shooter.Input;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterSprintingActivatingSystem : ISystem
    {
        private Entity _characterEntity;
        private Entity _inputEntity;

        public World World { get; set; }

        public void OnAwake()
        {
            var characterFilter = World.Filter.With<CharacterMovingComponent>();
            var movementInputFilter = World.Filter.With<MovementInputComponent>();

            _characterEntity = characterFilter.FirstOrDefault();
            _inputEntity = movementInputFilter.FirstOrDefault();
        }

        public void OnUpdate(float deltaTime)
        {
            if (_characterEntity == null || _inputEntity == null)
                return;
            
            ref var sprinting = ref _characterEntity.GetComponent<CharacterSprintingComponent>();
            ref var grounded = ref _characterEntity.GetComponent<CharacterGroundedComponent>();
            ref var input = ref _inputEntity.GetComponent<MovementInputComponent>();

            if (input.IsSprintKeyPressed && sprinting.CanSprint && grounded.IsActive && input.Vector != Vector2.zero)
                sprinting.IsActive = true;

            if (input.IsSprintedKeyReleasedThisFrame || !sprinting.CanSprint) 
                sprinting.IsActive = false;
        }
        
        public void Dispose()  { }
    }
}
using System;
using Scellecs.Morpeh;

namespace Shooter.Input
{
    public sealed class CrouchingInputReadingSystem : ISystem
    {
        private readonly CharacterControls _characterControls;

        public CrouchingInputReadingSystem(CharacterControls characterControls)
            => _characterControls = characterControls ?? throw new ArgumentNullException(nameof(characterControls));
        
        public World World { get; set; }
        
        public void Dispose() 
            => _characterControls.Dispose();

        public void OnAwake()
        {
            _characterControls.Enable();
            var filter = World.Filter.With<CrouchingInputComponent>();

            foreach (var entity in filter)
            {
                _characterControls.Character.Crouch.performed += context =>
                {
                    var isControlPressed = context.ReadValue<float>() > 0.1f;
                    entity.GetComponent<CrouchingInputComponent>().IsCrouchKeyPressedNow = isControlPressed;
                };
            }
        }

        public void OnUpdate(float deltaTime) { }
    }
}
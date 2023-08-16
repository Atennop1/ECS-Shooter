using System;
using Scellecs.Morpeh;

namespace Shooter.Input
{
    public sealed class JumpingInputReadingSystem : ISystem
    {
        private readonly CharacterControls _characterControls;

        public JumpingInputReadingSystem(CharacterControls characterControls)
            => _characterControls = characterControls ?? throw new ArgumentNullException(nameof(characterControls));
        
        public World World { get; set; }
        
        public void Dispose() 
            => _characterControls.Dispose();

        public void OnAwake()
        {
            _characterControls.Enable();
            var filter = World.Filter.With<JumpingInputComponent>();

            foreach (var entity in filter)
            {
                _characterControls.Character.Jump.performed += context =>
                {
                    var isJumpKeyDown = context.ReadValue<float>() > 0.1f;
                    entity.GetComponent<JumpingInputComponent>().IsJumpKeyPressedNow = isJumpKeyDown;
                };
            }
        }

        public void OnUpdate(float deltaTime) { }
    }
}
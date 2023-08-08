using System;
using Scellecs.Morpeh;
using UnityEngine;

namespace Shooter.Input
{
    public sealed class PlayerInputReadingSystem : ICleanupSystem
    {
        private readonly CharacterControls _characterControls;

        public PlayerInputReadingSystem(CharacterControls characterControls)
            => _characterControls = characterControls ?? throw new ArgumentNullException(nameof(characterControls));
        
        public World World { get; set; }
        
        public void Dispose() 
            => _characterControls.Dispose();

        public void OnAwake()
        {
            _characterControls.Enable();
            var filter = World.Filter.With<PlayerInputComponent>();

            foreach (var entity in filter)
            {
                _characterControls.Character.Movement.performed += context =>
                {
                    var movementInput = context.ReadValue<Vector2>();
                    entity.GetComponent<PlayerInputComponent>().MovementInput = movementInput;
                };
                
                _characterControls.Character.Jump.performed += context =>
                {
                    var isJumpKeyDown = context.ReadValue<float>() > 0.1f;
                    entity.GetComponent<PlayerInputComponent>().IsJumpKeyPressed = isJumpKeyDown;
                };
                
                _characterControls.Character.Sprint.performed += context =>
                {
                    var isShiftPressed = context.ReadValue<float>() > 0.1f;
                    entity.GetComponent<PlayerInputComponent>().IsShiftPressed = isShiftPressed;
                };
            }
        }

        public void OnUpdate(float deltaTime) { }
    }
}
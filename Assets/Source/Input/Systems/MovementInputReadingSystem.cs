using System;
using Scellecs.Morpeh;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Shooter.Input
{
    public sealed class MovementInputReadingSystem : ISystem
    {
        private readonly CharacterControls _characterControls;
        private Filter _filter;

        public MovementInputReadingSystem(CharacterControls characterControls)
            => _characterControls = characterControls ?? throw new ArgumentNullException(nameof(characterControls));
        
        public World World { get; set; }
        
        public void Dispose() 
            => _characterControls.Dispose();

        public void OnAwake()
        {
            _characterControls.Enable();
            _filter = World.Filter.With<MovementInputComponent>();

            foreach (var entity in _filter)
            {
                _characterControls.Character.Movement.performed += context =>
                {
                    var movementInput = context.ReadValue<Vector2>();
                    entity.GetComponent<MovementInputComponent>().Vector = movementInput;
                };
            }
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref var input = ref entity.GetComponent<MovementInputComponent>();
                input.IsSprintKeyPressed = Keyboard.current.shiftKey.isPressed;
                input.IsSprintedKeyReleasedThisFrame = Keyboard.current.shiftKey.wasReleasedThisFrame;
            }
        }
    }
}
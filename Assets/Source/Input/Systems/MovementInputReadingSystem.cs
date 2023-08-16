using System;
using Scellecs.Morpeh;
using UnityEngine;

namespace Shooter.Input
{
    public sealed class MovementInputReadingSystem : ISystem
    {
        private readonly CharacterControls _characterControls;

        public MovementInputReadingSystem(CharacterControls characterControls)
            => _characterControls = characterControls ?? throw new ArgumentNullException(nameof(characterControls));
        
        public World World { get; set; }
        
        public void Dispose() 
            => _characterControls.Dispose();

        public void OnAwake()
        {
            _characterControls.Enable();
            var filter = World.Filter.With<MovementInputComponent>();

            foreach (var entity in filter)
            {
                _characterControls.Character.Movement.performed += context =>
                {
                    var movementInput = context.ReadValue<Vector2>();
                    entity.GetComponent<MovementInputComponent>().Vector = movementInput;
                };
                
                _characterControls.Character.Sprint.performed += context =>
                {
                    var isShiftPressed = context.ReadValue<float>() > 0.1f;
                    entity.GetComponent<MovementInputComponent>().IsSprintKeyPressed = isShiftPressed;
                };
            }
        }

        public void OnUpdate(float deltaTime) { }
    }
}
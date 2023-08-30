using System;
using Scellecs.Morpeh;
using UnityEngine;

namespace Shooter.Input
{
    public sealed class MovingInputReadingSystem : ISystem
    {
        private readonly CharacterControls _characterControls;

        public MovingInputReadingSystem(CharacterControls characterControls)
            => _characterControls = characterControls ?? throw new ArgumentNullException(nameof(characterControls));
        
        public World World { get; set; }

        public void Dispose() 
            => _characterControls.Dispose();

        public void OnAwake()
        {
            _characterControls.Enable();
            var filter = World.Filter.With<MovingInputComponent>();

            foreach (var entity in filter)
            {
                _characterControls.Character.Movement.performed += context =>
                {
                    var movingInput = context.ReadValue<Vector2>();
                    entity.GetComponent<MovingInputComponent>().Vector = movingInput;
                };
            }
        }
        
        public void OnUpdate(float deltaTime) { }
    }
}
﻿using Scellecs.Morpeh;
using UnityEngine.InputSystem;

namespace Shooter.Input.Aim
{
    public sealed class AimInputReadingSystem : ISystem
    {
        private Entity _inputEntity;

        public World World { get; set; }

        public void OnAwake()
        {
            var inputFilter = World.Filter.With<AimInputComponent>();
            _inputEntity = inputFilter.FirstOrDefault();
        }

        public void OnUpdate(float deltaTime)
        {
            if (_inputEntity == null)
                return;

            ref var input = ref _inputEntity.GetComponent<AimInputComponent>();
            input.IsAimButtonPressedNow = Mouse.current.rightButton.isPressed;
        }
        
        public void Dispose() { }
    }
}
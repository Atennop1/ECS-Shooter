﻿using Scellecs.Morpeh;
using UnityEngine.InputSystem;

namespace Shooter.Input
{
    public sealed class CrouchingInputReadingSystem : ISystem
    {
        private Entity _inputEntity;

        public World World { get; set; }

        public void OnAwake()
        {
            var inputFilter = World.Filter.With<CrouchingInputComponent>();
            _inputEntity = inputFilter.FirstOrDefault();
        }

        public void OnUpdate(float deltaTime)
        {
            if (_inputEntity == null)
                return;

            ref var input = ref _inputEntity.GetComponent<CrouchingInputComponent>();
            input.IsCrouchKeyPressedThisFrame = Keyboard.current.leftCtrlKey.wasPressedThisFrame;
        }
        
        public void Dispose() { }
    }
}
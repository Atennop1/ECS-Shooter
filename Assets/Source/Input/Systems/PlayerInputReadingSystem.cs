using System;
using Leopotam.EcsLite;
using UnityEngine;

namespace Shooter.Input
{
    public sealed class PlayerInputReadingSystem : IEcsInitSystem, IEcsDestroySystem
    {
        private readonly CharacterControls _characterControls;

        public PlayerInputReadingSystem(CharacterControls characterControls)
            => _characterControls = characterControls ?? throw new ArgumentNullException(nameof(characterControls));

        public void Init(IEcsSystems systems)
        {
            _characterControls.Enable();
            
            var world = systems.GetWorld();
            var filter = world.Filter<PlayerInput>().End();
            var pool = world.GetPool<PlayerInput>();

            foreach (var entity in filter)
            {
                _characterControls.Character.Movement.performed += context =>
                {
                    var movementInput = context.ReadValue<Vector2>();
                    pool.Get(entity).MovementInput = movementInput;
                };
                
                _characterControls.Character.Jump.performed += context =>
                {
                    var isJumpKeyDown = context.ReadValue<float>() > 0.1f;
                    pool.Get(entity).IsJumpKeyPressed = isJumpKeyDown;
                };
                
                _characterControls.Character.Sprint.performed += context =>
                {
                    var isShiftPressed = context.ReadValue<float>() > 0.1f;
                    pool.Get(entity).IsShiftPressed = isShiftPressed;
                };
            }
        }
        
        public void Destroy(IEcsSystems systems)
            => _characterControls.Disable();
    }
}
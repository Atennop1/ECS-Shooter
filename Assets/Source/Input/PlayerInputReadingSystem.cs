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
            var filter = world.Filter<PlayerInputData>().End();
            var pool = world.GetPool<PlayerInputData>();

            foreach (var entity in filter)
            {
                _characterControls.Character.Movement.performed += context =>
                {
                    var movementInput = context.ReadValue<Vector2>();
                    pool.Get(entity).MovementInput = movementInput;
                    
                    Debug.Log(movementInput);
                };
            }
        }
        
        public void Destroy(IEcsSystems systems)
            => _characterControls.Disable();
    }
}
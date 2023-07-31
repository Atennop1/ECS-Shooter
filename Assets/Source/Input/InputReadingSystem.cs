using System;
using Leopotam.EcsLite;
using UnityEngine;

namespace Shooter.Input
{
    public sealed class InputReadingSystem : IEcsInitSystem, IEcsDestroySystem
    {
        private readonly CharacterControls _characterControls;

        public InputReadingSystem(CharacterControls characterControls)
            => _characterControls = characterControls ?? throw new ArgumentNullException(nameof(characterControls));

        public void Init(IEcsSystems systems)
        {
            _characterControls.Enable();
            
            var world = systems.GetWorld();
            var filter = world.Filter<InputData>().End();
            var pool = world.GetPool<InputData>();

            foreach (var entity in filter)
            {
                _characterControls.Character.Movement.performed += context =>
                {
                    var movingDirection = context.ReadValue<Vector2>();
                    pool.Get(entity).MovingDirection = movingDirection;
                };
            }
        }
        
        public void Destroy(IEcsSystems systems)
            => _characterControls.Disable();
    }
}
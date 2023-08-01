using System;
using Leopotam.EcsLite;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Shooter.Character
{
    public sealed class CharacterRotatingSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly Transform _characterTransform;
        private readonly Transform _cameraTransform;

        private float _xRotation;

        public CharacterRotatingSystem(Transform characterTransform, Transform cameraTransform)
        {
            _characterTransform = characterTransform ?? throw new ArgumentNullException(nameof(characterTransform));
            _cameraTransform = cameraTransform ?? throw new ArgumentNullException(nameof(cameraTransform));
        }

        public void Init(IEcsSystems systems) 
            => Cursor.lockState = CursorLockMode.Locked;

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var pool = world.GetPool<Character>();
            var filter = world.Filter<Character>().End();

            foreach (var entity in filter)
            {
                ref var character = ref pool.Get(entity);
                var rotatingDirection = Mouse.current.delta.ReadValue() * character.CameraMoving.MouseSensitivity * Time.deltaTime;

                _xRotation -= rotatingDirection.y;
                _xRotation = Mathf.Clamp(_xRotation, -90, 90);
                
                _cameraTransform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
                _characterTransform.Rotate(Vector3.up * rotatingDirection.x);
            }
        }
    }
}

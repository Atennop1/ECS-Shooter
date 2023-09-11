using System;
using Scellecs.Morpeh;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Shooter.Character
{
    public sealed class CharacterHeadMovingSystem : ISystem
    {
        private readonly Transform _characterTransform;

        private float _xRotation;
        private Entity _characterEntity;

        public CharacterHeadMovingSystem(Transform characterTransform) 
            => _characterTransform = characterTransform ?? throw new ArgumentNullException(nameof(characterTransform));

        public World World { get; set; }

        public void OnAwake()
        {
            Cursor.lockState = CursorLockMode.Locked;
            
            var filter = World.Filter.With<CharacterHeadMovingComponent>();
            _characterEntity = filter.FirstOrDefault();
        }

        public void OnUpdate(float deltaTime)
        {
            if (_characterEntity == null)
                return;
            
            ref var headMoving = ref _characterEntity.GetComponent<CharacterHeadMovingComponent>();
            var rotatingDirection = Mouse.current.delta.ReadValue() * headMoving.MouseSensitivity * Time.deltaTime;

            _xRotation -= rotatingDirection.y;
            _xRotation = Mathf.Clamp(_xRotation, -90, 90);
            _characterTransform.Rotate(Vector3.up * rotatingDirection.x);
        }

        public void Dispose() { }
    }
}

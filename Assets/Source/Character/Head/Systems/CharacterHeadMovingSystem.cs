using System;
using Scellecs.Morpeh;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Shooter.Character
{
    public sealed class CharacterHeadMovingSystem : ISystem
    {
        private readonly Transform _characterTransform;
        private readonly Transform _cameraTransform;
        private Entity _characterEntity;

        public CharacterHeadMovingSystem(Transform characterControllerTransform, Transform cameraTransform)
        {
            _characterTransform = characterControllerTransform ?? throw new ArgumentNullException(nameof(characterControllerTransform));
            _cameraTransform = cameraTransform ?? throw new ArgumentNullException(nameof(cameraTransform));
        }

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

            _cameraTransform.transform.rotation *= Quaternion.AngleAxis(rotatingDirection.y, Vector3.right);
            var localEulerAngles = _characterTransform.transform.localEulerAngles;
            var angles = new Vector3(localEulerAngles.x, localEulerAngles.y, 0);

            angles.x = angles.x switch
            {
                > 180 and < 340 => 340,
                < 180 and > 40 => 40,
                _ => angles.x
            };

            _characterTransform.transform.localEulerAngles = angles;
            _cameraTransform.rotation = Quaternion.Euler(0, _characterTransform.transform.rotation.eulerAngles.y, 0);
            _characterTransform.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
        }

        public void Dispose() { }
    }
}

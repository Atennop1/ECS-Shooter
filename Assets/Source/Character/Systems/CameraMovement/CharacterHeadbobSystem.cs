using System;
using Scellecs.Morpeh;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterHeadbobSystem : ISystem 
    {
        private readonly Transform _cameraTransform;
        private readonly float _defaultCameraPositionY;
        
        private float _timer;
        private Entity _characterEntity;

        public CharacterHeadbobSystem(Transform cameraTransform)
        {
            _cameraTransform = cameraTransform ?? throw new ArgumentNullException(nameof(cameraTransform));
            _defaultCameraPositionY = _cameraTransform.localPosition.y;
        }
        
        public World World { get; set; }

        public void OnAwake()
        {
            var filter = World.Filter.With<CharacterHeadMovingComponent>().With<CharacterMovingComponent>().With<CharacterJumpingComponent>();
            _characterEntity = filter.FirstOrDefault();
        }

        public void OnUpdate(float deltaTime)
        {
            if (_characterEntity == null)
                return;
            
            ref var headMoving = ref _characterEntity.GetComponent<CharacterHeadMovingComponent>();
            ref var moving = ref _characterEntity.GetComponent<CharacterMovingComponent>();
            ref var grounded = ref _characterEntity.GetComponent<CharacterGroundedComponent>();

            var walkingBobData = headMoving.WalkingBobData;
            var sprintingBobData = headMoving.SprintingBobData;

            if (!grounded.IsActive || moving is { IsSprinting: false, IsWalking: false })
                return;

            _timer += Time.deltaTime * (moving.IsSprinting ? sprintingBobData.BobSpeed : walkingBobData.BobSpeed);

            _cameraTransform.localPosition = new Vector3(
                _cameraTransform.localPosition.x,
                _defaultCameraPositionY + Mathf.Sin(_timer) * (moving.IsSprinting ? sprintingBobData.BobStrength : walkingBobData.BobStrength),
                _cameraTransform.localPosition.z);
        }

        public void Dispose() { }
    }
}
using System;
using Leopotam.EcsLite;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterHeadbobSystem : IEcsRunSystem
    {
        private readonly Transform _cameraTransform;
        private readonly float _defaultCameraPositionY;
        
        private float _timer;

        public CharacterHeadbobSystem(Transform cameraTransform)
        {
            _cameraTransform = cameraTransform ?? throw new ArgumentNullException(nameof(cameraTransform));
            _defaultCameraPositionY = _cameraTransform.localPosition.y;
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<CharacterHeadMoving>().Inc<CharacterMoving>().Inc<CharacterJumping>().End();
            
            var headMovingPool = world.GetPool<CharacterHeadMoving>();
            var movingPool = world.GetPool<CharacterMoving>();
            var jumpingPool = world.GetPool<CharacterJumping>();

            foreach (var entity in filter)
            {
                ref var headMoving = ref headMovingPool.Get(entity);
                ref var moving = ref movingPool.Get(entity);
                ref var jumping = ref jumpingPool.Get(entity);

                var walkingBobData = headMoving.WalkingBobData;
                var sprintingBobData = headMoving.SprintingBobData;

                if (!jumping.IsGrounded || moving is { IsSprinting: false, IsWalking: false })
                    continue;

                _timer += Time.deltaTime * (moving.IsSprinting ? sprintingBobData.BobSpeed : walkingBobData.BobSpeed);

                _cameraTransform.localPosition = new Vector3(
                    _cameraTransform.localPosition.x,
                   _defaultCameraPositionY + Mathf.Sin(_timer) * (moving.IsSprinting ? sprintingBobData.BobStrength : walkingBobData.BobStrength),
                    _cameraTransform.localPosition.z);
            }
        }
    }
}
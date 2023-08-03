using System;
using Leopotam.EcsLite;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterHeadbobSystem : IEcsRunSystem
    {
        private readonly CharacterController _characterController;
        private readonly Transform _cameraTransform;
        private readonly float _defaultCameraPositionY;
        
        private float _timer;

        public CharacterHeadbobSystem(CharacterController characterController, Transform cameraTransform)
        {
            _characterController = characterController ?? throw new ArgumentNullException(nameof(characterController));
            _cameraTransform = cameraTransform ?? throw new ArgumentNullException(nameof(cameraTransform));
            _defaultCameraPositionY = _cameraTransform.localPosition.y;
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var pool = world.GetPool<Character>();
            var filter = world.Filter<Character>().End();

            foreach (var entity in filter)
            {
                ref var character = ref pool.Get(entity);
                var walkingBobData = character.HeadMovingData.WalkingBobData;
                var sprintingBobData = character.HeadMovingData.SprintingBobData;

                if (!character.IsGrounded || (!character.MovingData.IsSprinting && !character.MovingData.IsWalking))
                    continue;

                _timer += Time.deltaTime * (character.MovingData.IsSprinting ? sprintingBobData.BobSpeed : walkingBobData.BobSpeed);

                _cameraTransform.localPosition = new Vector3(
                    _cameraTransform.localPosition.x,
                   _defaultCameraPositionY + Mathf.Sin(_timer) * (character.MovingData.IsSprinting ? sprintingBobData.BobStrength : walkingBobData.BobStrength),
                    _cameraTransform.localPosition.z);
            }
        }
    }
}
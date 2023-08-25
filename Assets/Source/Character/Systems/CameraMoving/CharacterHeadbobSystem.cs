﻿using System;
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
            var filter = World.Filter.With<CharacterHeadMovingComponent>().With<CharacterMovingComponent>().With<CharacterSprintingComponent>().With<CharacterGroundedComponent>().With<CharacterCrouchingComponent>();
            _characterEntity = filter.FirstOrDefault();
        }

        public void OnUpdate(float deltaTime)
        {
            if (_characterEntity == null)
                return;
            
            ref var headBob = ref _characterEntity.GetComponent<CharacterHeadBobComponent>();
            ref var moving = ref _characterEntity.GetComponent<CharacterMovingComponent>();
            ref var sprinting = ref _characterEntity.GetComponent<CharacterSprintingComponent>();
            ref var grounded = ref _characterEntity.GetComponent<CharacterGroundedComponent>();
            ref var crouching = ref _characterEntity.GetComponent<CharacterCrouchingComponent>();

            var walkingBobData = headBob.WalkingBobData;
            var sprintingBobData = headBob.SprintingBobData;

            if (!grounded.IsActive || crouching.IsTransiting || (!moving.IsWalking && !sprinting.IsActive))
                return;

            _timer += Time.deltaTime * (crouching.IsActive ? headBob.CrouchingBobData.BobSpeed : (sprinting.IsActive ? sprintingBobData.BobSpeed : walkingBobData.BobSpeed));
            var strength = crouching.IsActive ? headBob.CrouchingBobData.BobStrength : (sprinting.IsActive ? sprintingBobData.BobStrength : walkingBobData.BobStrength);

            _cameraTransform.localPosition = new Vector3(
                _cameraTransform.localPosition.x,
                _defaultCameraPositionY + Mathf.Sin(_timer) * strength,
                _cameraTransform.localPosition.z);
        }

        public void Dispose() { }
    }
}
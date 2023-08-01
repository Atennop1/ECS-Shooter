﻿using Leopotam.EcsLite;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterFactory : MonoBehaviour
    {
        [SerializeField] private CharacterMovementFactory _characterMovementFactory;
        [SerializeField] private CharacterGroundingSystemFactory characterGroundingSystemFactory;
        
        [Space] 
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Transform _cameraTransform;

        public int Create(IEcsSystems ecsSystems)
        {
            var world = ecsSystems.GetWorld();
            var entity = world.NewEntity();

            _characterMovementFactory.Create(ecsSystems, entity);

            ecsSystems.Add(new CharacterRotatingSystem(_characterController.transform, _cameraTransform));
            ecsSystems.Add(new CharacterMovingSystem(_characterController));
            ecsSystems.Add(characterGroundingSystemFactory.Create());
            ecsSystems.Add(new CharacterGravitationSystem(_characterController));
            ecsSystems.Add(new CharacterJumpingSystem(_characterController));
            
            return entity;
        }
    }
}
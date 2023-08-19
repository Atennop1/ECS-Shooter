﻿using System;
using Scellecs.Morpeh;
using Shooter.Character;
using Shooter.GameLoop;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Shooter.EntryPoint
{
    public sealed class CharacterInstaller : MonoInstaller
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Transform _cameraTransform;
        
        [Space]
        [SerializeField] private CharacterMovingComponentFactory _characterMovingFactory;
        [SerializeField] private CharacterSlidingComponentFactory _characterSlidingFactory;
        [SerializeField] private CharacterJumpingComponentFactory _characterJumpingFactory;
        [SerializeField] private CharacterHeadMovingComponentFactory _characterHeadMovingFactory;
        [SerializeField] private CharacterHeadBobComponentFactory _characterHeadBobFactory;
        [SerializeField] private CharacterCrouchingComponentFactory _characterCrouchingFactory;
        
        [Space]
        [SerializeField] private CharacterGroundingSystemFactory _characterGroundingSystemFactory;
        [SerializeField] private CharacterSprintingSystemFactory _characterSprintingSystemFactory;
        [SerializeField] private CharacterCrouchingSystemFactory _characterCrouchingSystemFactory;
        [SerializeField] private CharacterCrouchingApplyingSystemFactory _characterCrouchingApplyingSystemFactory;

        private World _world;
        private IGameLoop _gameLoop;

        [Inject]
        public void Construct(World world, IGameLoop gameLoop)
        {
            _world = world ?? throw new ArgumentNullException(nameof(world));
            _gameLoop = gameLoop ?? throw new ArgumentNullException(nameof(gameLoop));
        }

        public override void InstallBindings()
        {
            var entity = _world.CreateEntity();

            _characterMovingFactory.CreateFor(entity);
            _characterJumpingFactory.CreateFor(entity);
            _characterCrouchingFactory.CreateFor(entity);
            _characterSlidingFactory.CreateFor(entity);
            
            _characterHeadMovingFactory.CreateFor(entity);
            _characterHeadBobFactory.CreateFor(entity);
            entity.AddComponent<CharacterGroundedComponent>();

            _gameLoop.AddSystem(_characterGroundingSystemFactory.Create());
            _gameLoop.AddSystem(new CharacterSlidingSystem(_characterController));
            _gameLoop.AddSystem(_characterCrouchingSystemFactory.Create());
            
            _gameLoop.AddSystem(new CharacterRotatingSystem(_characterController.transform, _cameraTransform));
            _gameLoop.AddSystem(new CharacterHeadbobSystem(_cameraTransform));
            
            _gameLoop.AddSystem(new CharacterMovingSystem(_characterController));
            _gameLoop.AddSystem(_characterSprintingSystemFactory.Create());
            _gameLoop.AddSystem(_characterCrouchingApplyingSystemFactory.Create());

            _gameLoop.AddSystem(new CharacterGravitationSystem());
            _gameLoop.AddSystem(new CharacterJumpingSystem());
            _gameLoop.AddSystem(new CharacterVerticalVelocityApplyingSystem(_characterController));
        }
    }
}
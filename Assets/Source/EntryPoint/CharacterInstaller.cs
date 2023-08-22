﻿using System;
using Scellecs.Morpeh;
using Shooter.Character;
using Shooter.GameLoop;
using UnityEngine;
using Zenject;

namespace Shooter.EntryPoint
{
    public sealed class CharacterInstaller : MonoInstaller
    {
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private CharacterController _characterController;

        [Header("Components")]
        [SerializeField] private CharacterMovingComponentFactory _characterMovingFactory;
        [SerializeField] private CharacterJumpingComponentFactory _characterJumpingFactory;
        [SerializeField] private CharacterCrouchingComponentFactory _characterCrouchingFactory;
        [SerializeField] private CharacterSlidingComponentFactory _characterSlidingFactory;
        [SerializeField] private CharacterSprintingComponentFactory _characterSprintingFactory;
        
        [Space]
        [SerializeField] private CharacterStaminaComponentFactory _characterStaminaFactory;
        [SerializeField] private CharacterStaminaRegeneratingComponentFactory _characterStaminaRegeneratingFactory;
        
        [Space]
        [SerializeField] private CharacterHeadMovingComponentFactory _characterHeadMovingFactory;
        [SerializeField] private CharacterHeadBobComponentFactory _characterHeadBobFactory;
        
        [Header("Systems")]
        [SerializeField] private CharacterGroundingSystemFactory _characterGroundingSystemFactory;
        [SerializeField] private CharacterSprintingApplyingSystemFactory _characterSprintingApplyingSystemFactory;
        [SerializeField] private CharacterCrouchingActivatingSystemFactory _characterCrouchingActivatingSystemFactory;
        [SerializeField] private CharacterCrouchingApplyingSystemFactory _characterCrouchingApplyingSystemFactory;
        [SerializeField] private CharacterStaminaDisplayingSystemFactory _characterStaminaDisplayingSystemFactory;

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
            _characterSprintingFactory.CreateFor(entity);
            
            _characterStaminaFactory.CreateFor(entity);
            _characterStaminaRegeneratingFactory.CreateFor(entity);
            
            _characterHeadMovingFactory.CreateFor(entity);
            _characterHeadBobFactory.CreateFor(entity);

            entity.AddComponent<CharacterGroundedComponent>();
            _gameLoop.AddSystem(_characterGroundingSystemFactory.Create());

            _gameLoop.AddSystem(new CharacterStaminaUsingSystem());
            _gameLoop.AddSystem(new CharacterStaminaRegeneratingSystem());
            _gameLoop.AddSystem(_characterStaminaDisplayingSystemFactory.Create());
            
            _gameLoop.AddSystem(new CharacterSprintingActivatingSystem());
            _gameLoop.AddSystem(_characterSprintingApplyingSystemFactory.Create());

            _gameLoop.AddSystem(_characterCrouchingActivatingSystemFactory.Create());
            _gameLoop.AddSystem(_characterCrouchingApplyingSystemFactory.Create());
            
            _gameLoop.AddSystem(new CharacterMovingSystem(_characterController));
            _gameLoop.AddSystem(new CharacterJumpingSystem());
            _gameLoop.AddSystem(new CharacterSlidingSystem(_characterController));
            
            _gameLoop.AddSystem(new CharacterGravitationSystem());
            _gameLoop.AddSystem(new CharacterVerticalVelocityApplyingSystem(_characterController));
            
            _gameLoop.AddSystem(new CharacterRotatingSystem(_characterController.transform, _cameraTransform));
            _gameLoop.AddSystem(new CharacterHeadbobSystem(_cameraTransform));
        }
    }
}
﻿using System;
using Cinemachine;
using Scellecs.Morpeh;
using Shooter.Character;
using Shooter.Core.GameLoop;
using UnityEngine;
using Zenject;

namespace Shooter.Core
{
    public sealed class CharacterInstaller : MonoInstaller
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private CinemachineVirtualCamera[] _cameras;

        [Header("Components")]
        [SerializeField] private CharacterJumpingComponentFactory _characterJumpingFactory;
        [SerializeField] private CharacterCrouchingComponentFactory _characterCrouchingFactory;
        [SerializeField] private CharacterSlidingComponentFactory _characterSlidingFactory;
        [SerializeField] private CharacterSprintingComponentFactory _characterSprintingFactory;
        
        [Space]
        [SerializeField] private CharacterFootstepsComponentFactory _characterFootstepsFactory;
        
        [Space]
        [SerializeField] private CharacterStaminaComponentFactory _characterStaminaFactory;
        [SerializeField] private CharacterStaminaRegeneratingComponentFactory _characterStaminaRegeneratingFactory;
        
        [Space]
        [SerializeField] private CharacterHeadMovingComponentFactory _characterHeadMovingFactory;
        [SerializeField] private CharacterHeadBobComponentFactory _characterHeadBobFactory;
        
        [Header("Systems")]
        [SerializeField] private CharacterGroundingSystemFactory _characterGroundingSystemFactory;
        [SerializeField] private CharacterFootstepsPlayingSystemFactory _characterFootstepsPlayingSystemFactory;
        
        [Space]
        [SerializeField] private CharacterStaminaDisplayingSystemFactory _characterStaminaDisplayingSystemFactory;
        
        [Space]
        [SerializeField] private CharacterMovingActivatingSystemFactory _characterMovingActivatingSystemFactory;
        [SerializeField] private CharacterSprintingSystemFactory _characterSprintingSystemFactory;
        [SerializeField] private CharacterCrouchingSystemFactory _characterCrouchingSystemFactory;

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

            entity.AddComponent<CharacterMovingComponent>();
            entity.AddComponent<CharacterGroundedComponent>();
            
            _characterJumpingFactory.CreateFor(entity);
            _characterCrouchingFactory.CreateFor(entity);
            _characterSlidingFactory.CreateFor(entity);
            _characterSprintingFactory.CreateFor(entity);
            _characterFootstepsFactory.CreateFor(entity);
            
            _characterStaminaFactory.CreateFor(entity);
            _characterStaminaRegeneratingFactory.CreateFor(entity);
            
            _characterHeadMovingFactory.CreateFor(entity);
            _characterHeadBobFactory.CreateFor(entity);
            
            _gameLoop.AddSystem(new CharacterStaminaUsingSystem());
            _gameLoop.AddSystem(new CharacterStaminaRegeneratingSystem());
            _gameLoop.AddSystem(_characterStaminaDisplayingSystemFactory.Create());
            
            _gameLoop.AddSystem(_characterMovingActivatingSystemFactory.Create());
            _gameLoop.AddSystem(_characterSprintingSystemFactory.Create());
            _gameLoop.AddSystem(_characterCrouchingSystemFactory.Create());
            _gameLoop.AddSystem(new CharacterMovingApplyingSystem(_characterController));
            
            _gameLoop.AddSystem(_characterGroundingSystemFactory.Create());
            _gameLoop.AddSystem(_characterFootstepsPlayingSystemFactory.Create());
            
            _gameLoop.AddSystem(new CharacterJumpingSystem());
            _gameLoop.AddSystem(new CharacterSlidingSystem(_characterController));
            
            _gameLoop.AddSystem(new CharacterGravitationSystem());
            _gameLoop.AddSystem(new CharacterVerticalVelocityApplyingSystem(_characterController));
            
            _gameLoop.AddSystem(new CharacterHeadMovingSystem(_characterController.transform, _cameras[0].transform));
            _gameLoop.AddSystem(new CharacterHeadbobSystem(_cameras));
        }
    }
}
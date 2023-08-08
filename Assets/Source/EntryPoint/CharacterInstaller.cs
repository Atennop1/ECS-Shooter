using Scellecs.Morpeh;
using Shooter.Character;
using Shooter.GameLoop;
using UnityEngine;
using Zenject;

namespace Shooter.EntryPoint
{
    public sealed class CharacterInstaller : MonoInstaller
    {
        [SerializeField] private CharacterCollisionDetector _characterCollisionDetector;
        
        [Space]
        [SerializeField] private CharacterGroundingSystemFactory _characterGroundingSystemFactory;
        [SerializeField] private CharacterSprintingSystemFactory _characterSprintingSystemFactory;
        
        [Space]
        [SerializeField] private CharacterMovingComponentFactory _characterMovingFactory;
        [SerializeField] private CharacterSlidingComponentFactory _characterSlidingFactory;
        [SerializeField] private CharacterJumpingComponentFactory _characterJumpingFactory;
        [SerializeField] private CharacterHeadMovingComponentFactory _characterHeadMovingFactory;
        
        [Space] 
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Transform _cameraTransform;
        
        [Inject] private World _world;
        [Inject] private IGameLoop _gameLoop;

        public override void InstallBindings()
        {
            var entity = _world.CreateEntity();
            _characterCollisionDetector.Construct(_world);

            _characterMovingFactory.CreateFor(entity);
            _characterSlidingFactory.CreateFor(entity);
            entity.AddComponent<CharacterGroundedComponent>();
            _characterJumpingFactory.CreateFor(entity);
            _characterHeadMovingFactory.CreateFor(entity);

            _gameLoop.AddSystem(_characterGroundingSystemFactory.Create());
            
            _gameLoop.AddSystem(new CharacterDetectingSlideSystem(_characterController));
            _gameLoop.AddSystem(new CharacterSlidingSystem(_characterController));
            
            _gameLoop.AddSystem(new CharacterRotatingSystem(_characterController.transform, _cameraTransform));
            _gameLoop.AddSystem(new CharacterHeadbobSystem(_cameraTransform));
            
            _gameLoop.AddSystem(new CharacterMovingSystem(_characterController));
            _gameLoop.AddSystem(_characterSprintingSystemFactory.Create());

            _gameLoop.AddSystem(new CharacterGravitationSystem());
            _gameLoop.AddSystem(new CharacterJumpingSystem());
            _gameLoop.AddSystem(new CharacterVerticalVelocityApplyingSystem(_characterController));
        }
    }
}
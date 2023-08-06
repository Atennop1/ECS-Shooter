using Leopotam.EcsLite;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterFactory : MonoBehaviour
    {
        [SerializeField] private CharacterCollisionDetector _characterCollisionDetector;
        
        [Space]
        [SerializeField] private CharacterGroundingSystemFactory _characterGroundingSystemFactory;
        [SerializeField] private CharacterSprintingSystemFactory _characterSprintingSystemFactory;
        
        [Space]
        [SerializeField] private CharacterMovingFactory _characterMovingFactory;
        [SerializeField] private CharacterSlidingFactory _characterSlidingFactory;
        [SerializeField] private CharacterJumpingFactory _characterJumpingFactory;
        [SerializeField] private CharacterHeadMovingFactory _characterHeadMovingFactory;
        
        [Space] 
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Transform _cameraTransform;

        public void Create(IEcsSystems ecsSystems)
        {
            var world = ecsSystems.GetWorld();
            var entity = world.NewEntity();
            _characterCollisionDetector.Construct(world);

            _characterMovingFactory.Create(ecsSystems, entity);
            _characterSlidingFactory.Create(ecsSystems, entity);
            _characterJumpingFactory.Create(ecsSystems, entity);
            _characterHeadMovingFactory.Create(ecsSystems, entity);

            ecsSystems.Add(_characterGroundingSystemFactory.Create());
            
            ecsSystems.Add(new CharacterDetectingSlideSystem(_characterController));
            ecsSystems.Add(new CharacterSlidingSystem(_characterController));
            
            ecsSystems.Add(new CharacterRotatingSystem(_characterController.transform, _cameraTransform));
            ecsSystems.Add(new CharacterHeadbobSystem(_cameraTransform));
            
            ecsSystems.Add(new CharacterMovingSystem(_characterController));
            ecsSystems.Add(_characterSprintingSystemFactory.Create());

            ecsSystems.Add(new CharacterGravitationSystem());
            ecsSystems.Add(new CharacterJumpingSystem());
            ecsSystems.Add(new CharacterVerticalVelocityApplyingSystem(_characterController));
        }
    }
}
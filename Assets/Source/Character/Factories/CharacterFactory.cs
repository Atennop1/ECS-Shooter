using Scellecs.Morpeh;
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
        [SerializeField] private CharacterMovingComponentFactory _characterMovingFactory;
        [SerializeField] private CharacterSlidingComponentFactory _characterSlidingFactory;
        [SerializeField] private CharacterJumpingComponentFactory _characterJumpingFactory;
        [SerializeField] private CharacterHeadMovingComponentFactory _characterHeadMovingFactory;
        
        [Space] 
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Transform _cameraTransform;

        public void Create(World world, SystemsGroup systemsGroup)
        {
            var entity = world.CreateEntity();
            _characterCollisionDetector.Construct(world);

            _characterMovingFactory.CreateFor(entity);
            _characterSlidingFactory.CreateFor(entity);
            _characterJumpingFactory.CreateFor(entity);
            _characterHeadMovingFactory.CreateFor(entity);

            systemsGroup.AddSystem(_characterGroundingSystemFactory.Create());
            
            systemsGroup.AddSystem(new CharacterDetectingSlideSystem(_characterController));
            systemsGroup.AddSystem(new CharacterSlidingSystem(_characterController));
            
            systemsGroup.AddSystem(new CharacterRotatingSystem(_characterController.transform, _cameraTransform));
            systemsGroup.AddSystem(new CharacterHeadbobSystem(_cameraTransform));
            
            systemsGroup.AddSystem(new CharacterMovingSystem(_characterController));
            systemsGroup.AddSystem(_characterSprintingSystemFactory.Create());

            systemsGroup.AddSystem(new CharacterGravitationSystem());
            systemsGroup.AddSystem(new CharacterJumpingSystem());
            systemsGroup.AddSystem(new CharacterVerticalVelocityApplyingSystem(_characterController));
        }
    }
}
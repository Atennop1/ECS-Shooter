using Leopotam.EcsLite;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterFactory : MonoBehaviour
    {
        [SerializeField] private CharacterGroundingSystemFactory _characterGroundingSystemFactory;
        [SerializeField] private CharacterSprintingSystemFactory _characterSprintingSystemFactory;
        
        [Space]
        [SerializeField] private CharacterMovingFactory _characterMovingFactory;
        [SerializeField] private CharacterJumpingFactory _characterJumpingFactory;
        [SerializeField] private CharacterCameraRotatingFactory _characterCameraRotatingFactory;
        
        [Space] 
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Transform _cameraTransform;

        public void Create(IEcsSystems ecsSystems)
        {
            var world = ecsSystems.GetWorld();
            var entity = world.NewEntity();

            _characterMovingFactory.Create(ecsSystems, entity);
            _characterJumpingFactory.Create(ecsSystems, entity);
            _characterCameraRotatingFactory.Create(ecsSystems, entity);

            ecsSystems.Add(new CharacterRotatingSystem(_characterController.transform, _cameraTransform));
            ecsSystems.Add(new CharacterMovingSystem(_characterController));
            ecsSystems.Add(_characterSprintingSystemFactory.Create());
            ecsSystems.Add(_characterGroundingSystemFactory.Create());
            ecsSystems.Add(new CharacterGravitationSystem(_characterController));
            ecsSystems.Add(new CharacterJumpingSystem(_characterController));
        }
    }
}
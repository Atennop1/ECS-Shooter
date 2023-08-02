using Leopotam.EcsLite;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterFactory : MonoBehaviour
    {
        [SerializeField] private CharacterGroundingSystemFactory _characterGroundingSystemFactory;
        [SerializeField] private CharacterSprintingSystemFactory _characterSprintingSystemFactory;
        
        [Space]
        [SerializeField] private CharacterMovingDataFactory _characterMovingDataFactory;
        [SerializeField] private CharacterJumpingDataFactory _characterJumpingDataFactory;
        [SerializeField] private CharacterHeadMovingDataFactory _characterHeadMovingDataFactory;
        
        [Space] 
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Transform _cameraTransform;

        public void Create(IEcsSystems ecsSystems)
        {
            var world = ecsSystems.GetWorld();
            var entity = world.NewEntity();
            
            var characterPool = world.GetPool<Character>();
            characterPool.Add(entity);

            ref var character = ref characterPool.Get(entity);
            character.MovingData = _characterMovingDataFactory.Create();
            character.JumpingData = _characterJumpingDataFactory.Create();
            character.HeadMovingData = _characterHeadMovingDataFactory.Create();

            ecsSystems.Add(_characterGroundingSystemFactory.Create());
            
            ecsSystems.Add(new CharacterRotatingSystem(_characterController.transform, _cameraTransform));
            ecsSystems.Add(new CharacterHeadbobSystem(_characterController, _cameraTransform));
            
            ecsSystems.Add(new CharacterMovingSystem(_characterController));
            ecsSystems.Add(_characterSprintingSystemFactory.Create());

            ecsSystems.Add(new CharacterGravitationSystem(_characterController));
            ecsSystems.Add(new CharacterJumpingSystem(_characterController));
            ecsSystems.Add(new CharacterVerticalVelocityApplyingSystem(_characterController));
        }
    }
}
using Leopotam.EcsLite;
using UnityEngine;
using UnityEngine.Serialization;

namespace Shooter.Character
{
    public sealed class CharacterFactory : MonoBehaviour
    {
        [SerializeField] private CharacterGroundingSystemFactory _characterGroundingSystemFactory;
        [SerializeField] private CharacterSprintingSystemFactory _characterSprintingSystemFactory;
        
        [Space]
        [SerializeField] private CharacterMovingFactory _characterMovingFactory;
        [SerializeField] private CharacterJumpingFactory _characterJumpingFactory;
        [SerializeField] private CharacterCameraMovingFactory characterCameraMovingFactory;
        
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
            character.Moving = _characterMovingFactory.Create();
            character.Jumping = _characterJumpingFactory.Create();
            character.CameraMoving = characterCameraMovingFactory.Create();

            ecsSystems.Add(new CharacterRotatingSystem(_characterController.transform, _cameraTransform));
            ecsSystems.Add(new CharacterMovingSystem(_characterController));
            ecsSystems.Add(_characterSprintingSystemFactory.Create());
            ecsSystems.Add(_characterGroundingSystemFactory.Create());
            ecsSystems.Add(new CharacterGravitationSystem(_characterController));
            ecsSystems.Add(new CharacterJumpingSystem(_characterController));
        }
    }
}
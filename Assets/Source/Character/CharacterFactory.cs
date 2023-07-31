using Leopotam.EcsLite;
using Shooter.Input;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterFactory : MonoBehaviour
    {
        [SerializeField] private float _mouseSensitivity;
        [SerializeField] private float _speed;

        [Space] 
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Transform _characterTransform;
        [SerializeField] private Transform _cameraTransform;

        public void Create(IEcsSystems ecsSystems)
        {
            var world = ecsSystems.GetWorld();
            var entity = world.NewEntity();
            
            var playerInputPool = world.GetPool<PlayerInputData>();
            playerInputPool.Add(entity);

            var characterMovementPool = world.GetPool<CharacterMovementData>();
            characterMovementPool.Add(entity);

            ref var createdCharacterMovementData = ref characterMovementPool.Get(entity);
            createdCharacterMovementData.Speed = _speed;
            createdCharacterMovementData.MouseSensitivity = _mouseSensitivity;
            
            ecsSystems.Add(new PlayerInputReadingSystem(new CharacterControls()));
            ecsSystems.Add(new CharacterRotatingSystem(_characterTransform, _cameraTransform));
            ecsSystems.Add(new CharacterMovingSystem(_characterController));
        }
    }
}
using Leopotam.EcsLite;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterMovementFactory : MonoBehaviour
    {
        [SerializeField] private float _jumpHeight;
        [SerializeField] private float _gravitationalConstant;
        
        [Space]
        [SerializeField] private float _speed;
        [SerializeField] private float _mouseSensitivity;

        public CharacterMovement Create(IEcsSystems systems, int entity)
        {
            var world = systems.GetWorld();
            var pool = world.GetPool<CharacterMovement>();
            pool.Add(entity);

            ref var createdCharacterMovement = ref pool.Get(entity);
            
            createdCharacterMovement.JumpHeight = _jumpHeight;
            createdCharacterMovement.GravitationalConstant = _gravitationalConstant;
            createdCharacterMovement.Speed = _speed;
            createdCharacterMovement.MouseSensitivity = _mouseSensitivity;

            return createdCharacterMovement;
        }
    }
}
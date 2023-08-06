using Leopotam.EcsLite;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterMovingFactory : MonoBehaviour
    {
        [SerializeField] private float _speed;

        public void Create(IEcsSystems ecsSystems, int entity)
        {
            var world = ecsSystems.GetWorld();
            var pool = world.GetPool<CharacterMoving>();

            pool.Add(entity);
            ref var createdComponent = ref pool.Get(entity);
            
            createdComponent.Speed = _speed;
        }
    }
}
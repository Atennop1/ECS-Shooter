using Leopotam.EcsLite;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterMovingFactory : MonoBehaviour
    {
        [SerializeField] private float _speed;

        public CharacterMoving Create(IEcsSystems systems, int entity)
        {
            var world = systems.GetWorld();
            var pool = world.GetPool<CharacterMoving>();
            pool.Add(entity);

            ref var createdMoving = ref pool.Get(entity);
            createdMoving.Speed = _speed;

            return createdMoving;
        }
    }
}
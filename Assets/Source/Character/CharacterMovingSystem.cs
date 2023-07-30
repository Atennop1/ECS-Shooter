using System;
using Leopotam.EcsLite;
using Shooter.Input;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterMovingSystem : IEcsRunSystem
    {
        private readonly Rigidbody _rigidbody;

        public CharacterMovingSystem(Rigidbody rigidbody)
            => _rigidbody = rigidbody ?? throw new ArgumentNullException(nameof(rigidbody));

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var pool = world.GetPool<InputData>();
            var filter = world.Filter<InputData>().End();

            foreach (var entity in filter) 
                _rigidbody.velocity = new Vector3(pool.Get(entity).MovingDirection.x, 0, pool.Get(entity).MovingDirection.y);
        }
    }
}
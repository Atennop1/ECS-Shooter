using Leopotam.EcsLite;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterVerticalVelocityApplyingSystem : IEcsRunSystem
    {
        private readonly CharacterController _characterController;

        public CharacterVerticalVelocityApplyingSystem(CharacterController characterController)
            => _characterController = characterController;

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var pool = world.GetPool<Character>();
            var filter = world.Filter<Character>().End();

            foreach (var entity in filter)
                _characterController.Move(new Vector3(0, pool.Get(entity).MovingData.Velocity.y, 0) * Time.deltaTime);
        }
    }
}
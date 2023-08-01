using Leopotam.EcsLite;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterJumpingFactory : MonoBehaviour
    {
        [SerializeField] private float _jumpHeight;
        [SerializeField] private float _gravitationalConstant;

        public CharacterJumping Create(IEcsSystems systems, int entity)
        {
            var world = systems.GetWorld();
            var pool = world.GetPool<CharacterJumping>();
            pool.Add(entity);

            ref var createdJumping = ref pool.Get(entity);
            createdJumping.JumpHeight = _jumpHeight;
            createdJumping.GravitationalConstant = _gravitationalConstant;

            return createdJumping;
        }
    }
}
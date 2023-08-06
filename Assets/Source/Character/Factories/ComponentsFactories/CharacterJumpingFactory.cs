using Leopotam.EcsLite;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterJumpingFactory : MonoBehaviour
    {
        [SerializeField] private float _jumpHeight;
        [SerializeField] private float _gravitationalConstant;

        public void Create(IEcsSystems ecsSystems, int entity)
        {
            var world = ecsSystems.GetWorld();
            var pool = world.GetPool<CharacterJumping>();

            pool.Add(entity);
            ref var createdComponent = ref pool.Get(entity);

            createdComponent.JumpHeight = _jumpHeight;
            createdComponent.GravitationalConstant = _gravitationalConstant;
        }
    }
}
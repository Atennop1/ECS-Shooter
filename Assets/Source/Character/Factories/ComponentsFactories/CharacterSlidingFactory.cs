using Leopotam.EcsLite;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterSlidingFactory : MonoBehaviour
    {
        [SerializeField] private float _slideSpeed;

        public void Create(IEcsSystems ecsSystems, int entity)
        {
            var world = ecsSystems.GetWorld();
            var pool = world.GetPool<CharacterSliding>();

            pool.Add(entity);
            ref var createdComponent = ref pool.Get(entity);
            
            createdComponent.SlideSpeed = _slideSpeed;
        }
    }
}
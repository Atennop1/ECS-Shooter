using Leopotam.EcsLite;
using UnityEngine;

namespace Shooter.Input
{
    public sealed class PlayerInputFactory : MonoBehaviour
    {
        public PlayerInput Create(IEcsSystems ecsSystems, int entity)
        {
            var world = ecsSystems.GetWorld();
            var pool = world.GetPool<PlayerInput>();
            
            pool.Add(entity);
            ecsSystems.Add(new PlayerInputReadingSystem(new CharacterControls()));
            return pool.Get(entity);
        }
    }
}
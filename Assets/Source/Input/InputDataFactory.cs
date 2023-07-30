using Leopotam.EcsLite;
using UnityEngine;

namespace Shooter.Input
{
    public sealed class InputDataFactory : MonoBehaviour
    {
        public InputData Create(IEcsSystems ecsSystems)
        {
            var world = ecsSystems.GetWorld();
            var pool = world.GetPool<InputData>();

            var entity = world.NewEntity();
            pool.Add(entity);
            
            ecsSystems.Add(new InputReadingSystem(new CharacterControls()));
            return pool.Get(entity);
        }
    }
}
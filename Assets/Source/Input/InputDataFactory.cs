using Leopotam.EcsLite;
using UnityEngine;

namespace Shooter.Input
{
    public sealed class InputDataFactory : MonoBehaviour
    {
        [SerializeField] private float _mouseSensitivity;
        
        public InputData Create(IEcsSystems ecsSystems)
        {
            var world = ecsSystems.GetWorld();
            var pool = world.GetPool<InputData>();

            var entity = world.NewEntity();
            pool.Add(entity);

            ref var createdInputData = ref pool.Get(entity);
            createdInputData.MouseSensitivity = _mouseSensitivity;
            
            ecsSystems.Add(new InputReadingSystem(new CharacterControls()));
            return pool.Get(entity);
        }
    }
}
using Leopotam.EcsLite;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterCameraRotatingFactory : MonoBehaviour
    {
        [SerializeField] private float _mouseSensitivity;

        public CharacterCameraRotating Create(IEcsSystems systems, int entity)
        {
            var world = systems.GetWorld();
            var pool = world.GetPool<CharacterCameraRotating>();
            pool.Add(entity);

            ref var createdRotating = ref pool.Get(entity);
            createdRotating.MouseSensitivity = _mouseSensitivity;

            return createdRotating;
        }
    }
}
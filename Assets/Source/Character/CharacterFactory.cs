using Leopotam.EcsLite;
using Shooter.Input;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterFactory : MonoBehaviour
    {
        [SerializeField] private InputDataFactory _inputDataFactory;

        [Space] 
        [SerializeField] private Transform _characterTransform;
        [SerializeField] private Transform _cameraTransform;

        public void Create(IEcsSystems ecsSystems)
        {
            _inputDataFactory.Create(ecsSystems);
            ecsSystems.Add(new CharacterRotationSystem(_characterTransform, _cameraTransform));
        }
    }
}
using Leopotam.EcsLite;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterHeadMovingFactory : MonoBehaviour
    {
        [SerializeField] private float _mouseSensitivity;
        
        [Space]
        [SerializeField] private CharacterHeadbobData _walkingBobData;
        [SerializeField] private CharacterHeadbobData _sprintingBobData;

        public void Create(IEcsSystems ecsSystems, int entity)
        {
            var world = ecsSystems.GetWorld();
            var pool = world.GetPool<CharacterHeadMoving>();

            pool.Add(entity);
            ref var createdComponent = ref pool.Get(entity);
            
            createdComponent.MouseSensitivity = _mouseSensitivity; 
            createdComponent.WalkingBobData = _walkingBobData;
            createdComponent.SprintingBobData = _sprintingBobData;
        }
    }
}
using Scellecs.Morpeh;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterMovingComponentFactory : MonoBehaviour
    {
        [SerializeField] private float _speed;

        public void CreateFor(Entity entity)
        {
            ref var createdComponent = ref entity.AddComponent<CharacterMovingComponent>();
            createdComponent.Speed = _speed;
        }
    }
}
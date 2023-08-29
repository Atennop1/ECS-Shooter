using Scellecs.Morpeh;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterSlidingComponentFactory : MonoBehaviour
    {
        [SerializeField] private float _slideSpeed;

        public void CreateFor(Entity entity)
        {
            ref var createdComponent = ref entity.AddComponent<CharacterSlidingComponent>();
            createdComponent.SlideSpeed = _slideSpeed;
        }
    }
}
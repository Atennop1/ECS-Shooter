using Scellecs.Morpeh;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterHeadMovingComponentFactory : MonoBehaviour
    {
        [SerializeField] private float _mouseSensitivity;

        public void CreateFor(Entity entity)
        {
            ref var createdComponent = ref entity.AddComponent<CharacterHeadMovingComponent>();
            createdComponent.MouseSensitivity = _mouseSensitivity;
        }
    }
}
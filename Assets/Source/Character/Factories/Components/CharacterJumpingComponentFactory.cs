using Scellecs.Morpeh;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterJumpingComponentFactory : MonoBehaviour
    {
        [SerializeField] private float _jumpHeight;
        [SerializeField] private float _gravitationalConstant;

        public void CreateFor(Entity entity)
        {
            ref var createdComponent = ref entity.AddComponent<CharacterJumpingComponent>();

            createdComponent.JumpHeight = _jumpHeight;
            createdComponent.GravitationalConstant = _gravitationalConstant;
        }
    }
}
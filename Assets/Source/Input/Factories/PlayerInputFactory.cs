using Scellecs.Morpeh;
using UnityEngine;

namespace Shooter.Input
{
    public sealed class PlayerInputFactory : MonoBehaviour
    {
        public void Create(World world, SystemsGroup systemsGroup)
        {
            var entity = world.CreateEntity();
            entity.AddComponent<PlayerInputComponent>();
            systemsGroup.AddSystem(new PlayerInputReadingSystem(new CharacterControls()));
        }
    }
}
using Scellecs.Morpeh;
using Sirenix.OdinInspector;

namespace Shooter.Character
{
    public sealed class CharacterSprintingComponentFactory : SerializedMonoBehaviour
    {
        public void CreateFor(Entity entity)
        {
            ref var createdComponent = ref entity.AddComponent<CharacterSprintingComponent>();
            createdComponent.CanSprint = true;
        }
    }
}
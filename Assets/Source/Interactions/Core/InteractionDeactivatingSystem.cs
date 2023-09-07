using Scellecs.Morpeh;

namespace Shooter.Interactions
{
    public sealed class InteractionDeactivatingSystem : ISystem
    {
        private Filter _filter;
        
        public World World { get; set; }

        public void OnAwake() 
            => _filter = World.Filter.With<InteractableComponent>().With<InteractableActivatedComponent>();

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter) 
                entity.RemoveComponent<InteractableActivatedComponent>();
        }
        
        public void Dispose() { }
    }
}
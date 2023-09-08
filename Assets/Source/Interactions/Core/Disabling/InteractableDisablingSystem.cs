using Scellecs.Morpeh;

namespace Shooter.Interactions
{
    public sealed class InteractableDisablingSystem : ISystem
    {
        private Filter _filter;
        
        public World World { get; set; }

        public void OnAwake() 
            => _filter = World.Filter.With<InteractableComponent>().With<InteractableActivatedComponent>().With<DisableInteractableAfterUseComponent>();

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
                entity.GetComponent<InteractableComponent>().CanInteract = false;
        }
        
        public void Dispose() { }
    }
}
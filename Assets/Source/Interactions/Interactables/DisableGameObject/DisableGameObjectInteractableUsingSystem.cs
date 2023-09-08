using Scellecs.Morpeh;

namespace Shooter.Interactions
{
    public sealed class DisableGameObjectInteractableUsingSystem : ISystem
    {
        private Filter _filter;
        
        public World World { get; set; }

        public void OnAwake() 
            => _filter = World.Filter.With<InteractableComponent>().With<DisableGameObjectInteractableComponent>().With<InteractableActivatedComponent>();

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref var disableGameObjectInteractable = ref entity.GetComponent<DisableGameObjectInteractableComponent>();
                ref var interactable = ref entity.GetComponent<InteractableComponent>();
                
                disableGameObjectInteractable.GameObjectToDisable.SetActive(false);
                interactable.CanInteract = false;
            }
        }
        
        public void Dispose() { }
    }
}
using Scellecs.Morpeh;
using UnityEngine;

namespace Shooter.Interactions
{
    public sealed class LogInteractableActivating : ISystem
    {
        private Filter _filter;
        
        public World World { get; set; }

        public void OnAwake() 
            => _filter = World.Filter.With<InteractableComponent>().With<LogInteractableComponent>().With<InteractableActivatedComponent>();

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref var logInteractable = ref entity.GetComponent<LogInteractableComponent>();
                ref var interactable = ref entity.GetComponent<InteractableComponent>();
                
                Debug.Log(logInteractable.LogText);
                interactable.CanInteract = false;
                entity.RemoveComponent<InteractableActivatedComponent>();
            }
        }
        
        public void Dispose() { }
    }
}
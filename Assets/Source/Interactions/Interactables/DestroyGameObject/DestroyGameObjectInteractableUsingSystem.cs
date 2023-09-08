using Scellecs.Morpeh;
using UnityEngine;

namespace Shooter.Interactions
{
    public sealed class DestroyGameObjectInteractableUsingSystem : ISystem
    {
        private Filter _filter;
        
        public World World { get; set; }

        public void OnAwake() 
            => _filter = World.Filter.With<InteractableComponent>().With<DestroyGameObjectInteractableComponent>().With<InteractableActivatedComponent>();

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref var disableGameObjectInteractable = ref entity.GetComponent<DestroyGameObjectInteractableComponent>();
                Object.Destroy(disableGameObjectInteractable.GameObjectToDestroy);
            }
        }
        
        public void Dispose() { }
    }
}
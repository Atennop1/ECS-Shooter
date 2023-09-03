using Scellecs.Morpeh;
using UnityEngine;

namespace Shooter.Interactions
{
    public sealed class InteractionDetectingSystem : ISystem
    {
        private readonly Transform _characterHeadTransform;
        private readonly LayerMask _layerMask;
        private Entity _interactingEntity;
        
        public World World { get; set; }

        public void OnAwake()
        {
            var filter = World.Filter.With<InteractingComponent>();
            _interactingEntity = filter.FirstOrDefault();
        }

        public void OnUpdate(float deltaTime)
        {
            if (_interactingEntity == null)
                return;

            ref var interacting = ref _interactingEntity.GetComponent<InteractingComponent>();
            
            if (!UnityEngine.Physics.Raycast(_characterHeadTransform.position, _characterHeadTransform.forward, out var hit, interacting.InteractionRadius))
                return;
            
            if (hit.collider.gameObject.TryGetComponent<Interactable>(out var interactable))
                interacting.InteractableEntity = interactable.Entity;
        }
        
        public void Dispose() { }
    }
}
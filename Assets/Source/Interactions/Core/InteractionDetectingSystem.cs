using System;
using JetBrains.Annotations;
using Scellecs.Morpeh;
using UnityEngine;

namespace Shooter.Interactions
{
    public sealed class InteractionDetectingSystem : ISystem
    {
        private readonly Transform _characterHeadTransform;
        private readonly LayerMask _layerMask;
        private Filter _filter;

        public InteractionDetectingSystem(Transform characterHeadTransform, LayerMask layerMask)
        {
            _characterHeadTransform = characterHeadTransform ?? throw new ArgumentNullException(nameof(characterHeadTransform));
            _layerMask = layerMask;
        }

        public World World { get; set; }

        public void OnAwake() 
            => _filter = World.Filter.With<InteractingComponent>();

        public void OnUpdate(float deltaTime)
        {
            var entity = _filter.FirstOrDefault();
            Debug.Log(_filter.IsEmpty());
            if (entity == null)
                return;

            ref var interacting = ref entity.GetComponent<InteractingComponent>();
            interacting.SelectedInteractableEntity = null;
            
            Debug.Log("ray");
            if (!UnityEngine.Physics.Raycast(_characterHeadTransform.position, _characterHeadTransform.forward, out var hit, interacting.InteractingDistance, _layerMask))
                return;
            
            Debug.Log("hit");
            if (hit.collider.gameObject.TryGetComponent<Interactable>(out var interactable))
                interacting.SelectedInteractableEntity = interactable.Entity;
        }
        
        public void Dispose() { }
    }
}
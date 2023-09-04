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
        private Entity _entity;

        public InteractionDetectingSystem(Transform characterHeadTransform, LayerMask layerMask)
        {
            _characterHeadTransform = characterHeadTransform ?? throw new ArgumentNullException(nameof(characterHeadTransform));
            _layerMask = layerMask;
        }

        public World World { get; set; }

        public void OnAwake()
        {
            var filter = World.Filter.With<InteractingComponent>();
            _entity = filter.FirstOrDefault();
        }

        public void OnUpdate(float deltaTime)
        {
            if (_entity == null)
                return;

            ref var interacting = ref _entity.GetComponent<InteractingComponent>();
            
            if (!UnityEngine.Physics.Raycast(_characterHeadTransform.position, _characterHeadTransform.forward, out var hit, interacting.InteractingDistance, _layerMask))
                return;
            
            if (hit.collider.gameObject.TryGetComponent<Interactable>(out var interactable))
                interacting.SelectedInteractableEntity = interactable.Entity;
        }
        
        public void Dispose() { }
    }
}
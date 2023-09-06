using System;
using Scellecs.Morpeh;
using UnityEngine;

namespace Shooter.Interactions
{
    public sealed class InteractionReadinessDisplayingSystem : ISystem
    {
        private readonly InteractionReadinessView _interactionReadinessView;
        private Entity _entity;

        public InteractionReadinessDisplayingSystem(InteractionReadinessView interactionReadinessView) 
            => _interactionReadinessView = interactionReadinessView ??throw new ArgumentNullException(nameof(interactionReadinessView));

        public World World { get; set; }
        
        public void OnAwake()
        {
            var filter = World.Filter.With<InteractingComponent>();
            _entity = filter.FirstOrDefault();
        }

        public void OnUpdate(float deltaTime)
        {
            var filter = World.Filter.With<InteractingComponent>();
            _entity = filter.FirstOrDefault();
            
            if (_entity == null)
                return;

            ref var interacting = ref _entity.GetComponent<InteractingComponent>();
            
            if (interacting.SelectedInteractableEntity != null)
            {
                _interactionReadinessView.DisplayReadiness(); 
                return;
            }
            
            _interactionReadinessView.DisplayUnreadiness();
        }
        
        public void Dispose() { }
    }
}
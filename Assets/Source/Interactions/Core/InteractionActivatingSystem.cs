using Scellecs.Morpeh;
using Shooter.Input;

namespace Shooter.Interactions
{
    public sealed class InteractionActivatingSystem : ISystem
    {
        private Entity _interactingEntity;
        private Entity _inputEntity;
        
        public World World { get; set; }

        public void OnAwake()
        {
            var interactingFilter = World.Filter.With<InteractingComponent>();
            var inputFilter = World.Filter.With<InteractingInputComponent>();

            _interactingEntity = interactingFilter.FirstOrDefault();
            _inputEntity = inputFilter.FirstOrDefault();
        }
        
        public void OnUpdate(float deltaTime)
        {
            if (_interactingEntity == null || _inputEntity == null)
                return;

            ref var interacting = ref _interactingEntity.GetComponent<InteractingComponent>();
            ref var input = ref _inputEntity.GetComponent<InteractingInputComponent>();

            if (input.IsInteractingKeyPressedThisFrame)
                interacting.SelectedInteractableEntity.AddComponent<InteractableActivatedComponent>();
        }
        
        public void Dispose() { }
    }
}
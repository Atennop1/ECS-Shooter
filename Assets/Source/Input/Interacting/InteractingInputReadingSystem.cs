using Scellecs.Morpeh;
using UnityEngine.InputSystem;

namespace Shooter.Input
{
    public sealed class InteractingInputReadingSystem : ISystem
    {
        private Entity _inputEntity;

        public World World { get; set; }

        public void OnAwake()
        {
            var inputFilter = World.Filter.With<InteractingInputComponent>();
            _inputEntity = inputFilter.FirstOrDefault();
        }

        public void OnUpdate(float deltaTime)
        {
            if (_inputEntity == null)
                return;

            ref var input = ref _inputEntity.GetComponent<InteractingInputComponent>();
            input.IsInteractingKeyPressedThisFrame = Keyboard.current.eKey.wasPressedThisFrame;
        }
        
        public void Dispose() { }
    }
}
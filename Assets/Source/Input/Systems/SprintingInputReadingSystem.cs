using Scellecs.Morpeh;
using UnityEngine.InputSystem;

namespace Shooter.Input
{
    public sealed class SprintingInputReadingSystem : ISystem
    {
        private Entity _inputEntity;

        public World World { get; set; }

        public void OnAwake()
        {
            var inputFilter = World.Filter.With<SprintingInputComponent>();
            _inputEntity = inputFilter.FirstOrDefault();
        }

        public void OnUpdate(float deltaTime)
        {
            if (_inputEntity == null)
                return;

            ref var input = ref _inputEntity.GetComponent<SprintingInputComponent>();
            input.IsSprintKeyPressed = Keyboard.current.shiftKey.isPressed;
            input.IsSprintedKeyReleasedThisFrame = Keyboard.current.shiftKey.wasReleasedThisFrame;
        }
        
        public void Dispose() { }
    }
}
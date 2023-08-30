using Scellecs.Morpeh;
using UnityEngine.InputSystem;

namespace Shooter.Input
{
    public sealed class JumpingInputReadingSystem : ISystem
    {
        private Entity _inputEntity;

        public World World { get; set; }

        public void OnAwake()
        {
            var inputFilter = World.Filter.With<JumpingInputComponent>();
            _inputEntity = inputFilter.FirstOrDefault();
        }

        public void OnUpdate(float deltaTime)
        {
            if (_inputEntity == null)
                return;

            ref var input = ref _inputEntity.GetComponent<JumpingInputComponent>();
            input.IsJumpKeyPressedThisFrame = Keyboard.current.spaceKey.wasPressedThisFrame;
        }
        
        public void Dispose() { }
    }
}
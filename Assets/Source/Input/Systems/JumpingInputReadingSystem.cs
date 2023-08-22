using Scellecs.Morpeh;
using UnityEngine.InputSystem;

namespace Shooter.Input
{
    public sealed class JumpingInputReadingSystem : ISystem
    {
        private Filter _filter;

        public World World { get; set; }

        public void OnAwake() 
            => _filter = World.Filter.With<JumpingInputComponent>();

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
                entity.GetComponent<JumpingInputComponent>().IsJumpKeyPressedThisFrame = Keyboard.current.spaceKey.wasPressedThisFrame;
        }
        
        public void Dispose() { }
    }
}
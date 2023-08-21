using Scellecs.Morpeh;
using UnityEngine.InputSystem;

namespace Shooter.Input
{
    public sealed class CrouchingInputReadingSystem : ISystem
    {
        private Filter _filter;

        public World World { get; set; }

        public void OnAwake() 
            => _filter = World.Filter.With<CrouchingInputComponent>();

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
                entity.GetComponent<CrouchingInputComponent>().IsCrouchKeyPressedNow = Keyboard.current.leftCtrlKey.wasPressedThisFrame;
        }
        
        public void Dispose() { }
    }
}
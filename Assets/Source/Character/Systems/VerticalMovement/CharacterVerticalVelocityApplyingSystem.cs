using Scellecs.Morpeh;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterVerticalVelocityApplyingSystem : ISystem
    {
        private readonly CharacterController _characterController;

        public CharacterVerticalVelocityApplyingSystem(CharacterController characterController)
            => _characterController = characterController;

        public World World { get; set; }
        
        public void OnUpdate(float deltaTime)
        {
            var filter = World.Filter.With<CharacterJumpingComponent>();
            var entity = filter.FirstOrDefault();
            
            if (entity != null) 
                _characterController.Move(new Vector3(0, entity.GetComponent<CharacterJumpingComponent>().VerticalVelocity, 0) * Time.deltaTime);
        }

        public void Dispose() { }
        public void OnAwake() { }
        
    }
}
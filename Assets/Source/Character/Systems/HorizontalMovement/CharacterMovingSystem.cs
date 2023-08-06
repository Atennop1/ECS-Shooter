using Scellecs.Morpeh;
using Shooter.Input;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterMovingSystem : ISystem
    {
        private readonly CharacterController _characterController;

        public CharacterMovingSystem(CharacterController characterController) 
            => _characterController = characterController;

        public World World { get; set; }

        public void OnUpdate(float deltaTime)
        {
            var movingFilter = World.Filter.With<CharacterMovingComponent>();
            var playerInputFilter = World.Filter.With<PlayerInputComponent>();

            var movingEntity = movingFilter.FirstOrDefault();
            var inputEntity = playerInputFilter.FirstOrDefault();

            if (movingEntity == null || inputEntity == null)
                return;
            
            ref var moving = ref movingEntity.GetComponent<CharacterMovingComponent>();
            ref var input = ref inputEntity.GetComponent<PlayerInputComponent>();

            var addedVelocity = _characterController.transform.right * input.MovementInput.x + _characterController.transform.forward * input.MovementInput.y;
            moving.IsWalking = addedVelocity != Vector3.zero;
            _characterController.Move(addedVelocity * moving.Speed * Time.deltaTime);
        }

        public void Dispose() { }
        public void OnAwake() { }
    }
}
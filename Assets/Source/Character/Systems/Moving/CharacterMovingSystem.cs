using Scellecs.Morpeh;
using Shooter.Input;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterMovingSystem : ISystem
    {
        private readonly CharacterController _characterController;

        private Entity _characterEntity;
        private Entity _inputEntity;

        public CharacterMovingSystem(CharacterController characterController) 
            => _characterController = characterController;

        public World World { get; set; }

        public void OnAwake()
        {
            var characterFilter = World.Filter.With<CharacterMovingComponent>();
            var movementInputFilter = World.Filter.With<MovementInputComponent>();

            _characterEntity = characterFilter.FirstOrDefault();
            _inputEntity = movementInputFilter.FirstOrDefault();
        }

        public void OnUpdate(float deltaTime)
        {
            if (_characterEntity == null || _inputEntity == null)
                return;

            ref var moving = ref _characterEntity.GetComponent<CharacterMovingComponent>();
            ref var input = ref _inputEntity.GetComponent<MovementInputComponent>();
            
            var addedVelocity = _characterController.transform.right * input.Vector.x + _characterController.transform.forward * input.Vector.y;
            moving.IsWalking = addedVelocity != Vector3.zero;
            _characterController.Move(addedVelocity * moving.Speed * Time.deltaTime);
        }

        public void Dispose() { }
    }
}
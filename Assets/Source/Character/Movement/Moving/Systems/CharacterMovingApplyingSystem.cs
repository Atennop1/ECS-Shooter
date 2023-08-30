using Scellecs.Morpeh;
using Shooter.Input;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterMovingApplyingSystem : ISystem
    {
        private readonly CharacterController _characterController;

        private Entity _characterEntity;
        private Entity _inputEntity;

        public CharacterMovingApplyingSystem(CharacterController characterController) 
            => _characterController = characterController;

        public World World { get; set; }

        public void OnAwake()
        {
            var characterFilter = World.Filter.With<CharacterMovingComponent>();
            var movementInputFilter = World.Filter.With<MovingInputComponent>();

            _characterEntity = characterFilter.FirstOrDefault();
            _inputEntity = movementInputFilter.FirstOrDefault();
        }

        public void OnUpdate(float deltaTime)
        {
            if (_characterEntity == null || _inputEntity == null)
                return;

            ref var moving = ref _characterEntity.GetComponent<CharacterMovingComponent>();
            ref var input = ref _inputEntity.GetComponent<MovingInputComponent>();
            
            var addedVelocity = _characterController.transform.right * input.Vector.x + _characterController.transform.forward * input.Vector.y;
            _characterController.Move(addedVelocity * moving.CurrentSpeed * Time.deltaTime);
        }

        public void Dispose() { }
    }
}
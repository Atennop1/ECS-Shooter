using Scellecs.Morpeh;
using Shooter.Input;
using Shooter.Tools;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterMovingActivatingSystem : ISystem
    {
        private readonly float _movingSpeed;
        
        private Entity _characterEntity;
        private Entity _inputEntity;

        public CharacterMovingActivatingSystem(float movingSpeed) 
            => _movingSpeed = movingSpeed.ThrowExceptionIfLessOrEqualsZero();

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
            
            moving.IsWalking = input.Vector != Vector2.zero;
            moving.CurrentSpeed = _movingSpeed;
        }

        public void Dispose() { }
    }
}
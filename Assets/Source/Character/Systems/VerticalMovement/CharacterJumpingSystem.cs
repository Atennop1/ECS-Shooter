using Scellecs.Morpeh;
using Shooter.Input;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterJumpingSystem : ISystem
    {
        private Entity _characterEntity;
        private Entity _inputEntity;
        
        public World World { get; set; }

        public void OnAwake()
        {
            var characterFilter = World.Filter.With<CharacterJumpingComponent>().With<CharacterSlidingComponent>().With<CharacterGroundedComponent>();
            var playerInputFilter = World.Filter.With<PlayerInputComponent>();

            _characterEntity = characterFilter.FirstOrDefault();
            _inputEntity = playerInputFilter.FirstOrDefault();
        }
        
        public void OnUpdate(float deltaTime)
        {
            if (_characterEntity == null || _inputEntity == null)
                return;
            
            ref var input = ref _inputEntity.GetComponent<PlayerInputComponent>();
            ref var jumping = ref _characterEntity.GetComponent<CharacterJumpingComponent>();
            ref var grounded = ref _characterEntity.GetComponent<CharacterGroundedComponent>();
            ref var sliding = ref _characterEntity.GetComponent<CharacterSlidingComponent>();

            if (!grounded.IsActive || sliding.IsActive || !input.IsJumpKeyPressed)
                return;

            jumping.VerticalVelocity = Mathf.Sqrt(-2 * jumping.JumpHeight * jumping.GravitationalConstant);
        }

        public void Dispose() { }
    }
}
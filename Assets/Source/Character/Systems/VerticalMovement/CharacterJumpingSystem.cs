using Scellecs.Morpeh;
using Shooter.Input;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterJumpingSystem : ISystem
    {
        public World World { get; set; }

        public void OnUpdate(float deltaTime)
        {
            var characterFilter = World.Filter.With<CharacterJumpingComponent>().With<CharacterSlidingComponent>().With<CharacterGroundedComponent>();
            var inputFilter = World.Filter.With<PlayerInputComponent>();

            var characterEntity = characterFilter.FirstOrDefault();
            var inputEntity = inputFilter.FirstOrDefault();

            if (characterEntity == null || inputEntity == null)
                return;

            ref var jumping = ref characterEntity.GetComponent<CharacterJumpingComponent>();
            ref var input = ref inputEntity.GetComponent<PlayerInputComponent>();

            if (!characterEntity.GetComponent<CharacterGroundedComponent>().IsActive || !input.IsJumpKeyPressed || characterEntity.GetComponent<CharacterSlidingComponent>().IsActive)
                return;

            jumping.VerticalVelocity = Mathf.Sqrt(-2 * jumping.JumpHeight * jumping.GravitationalConstant);
        }

        public void Dispose() { }
        public void OnAwake() { }
    }
}
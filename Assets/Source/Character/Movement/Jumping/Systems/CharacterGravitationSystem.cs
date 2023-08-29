using Scellecs.Morpeh;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterGravitationSystem : ISystem
    {
        private Entity _characterEntity;
        
        public World World { get; set; }
        
        public void OnAwake()
        {
            var filter = World.Filter.With<CharacterGroundedComponent>().With<CharacterSlidingComponent>().With<CharacterJumpingComponent>();
            _characterEntity = filter.FirstOrDefault();
        }

        public void OnUpdate(float deltaTime)
        {
            if (_characterEntity == null)
                return;

            ref var grounded = ref _characterEntity.GetComponent<CharacterGroundedComponent>();
            ref var sliding = ref _characterEntity.GetComponent<CharacterSlidingComponent>();
            ref var jumping = ref _characterEntity.GetComponent<CharacterJumpingComponent>();

            if (!grounded.IsActive || sliding.IsActive)
                jumping.VerticalVelocity += Physics.Constants.GravityAcceleration * Time.deltaTime;
        }

        public void Dispose() { }
    }
}
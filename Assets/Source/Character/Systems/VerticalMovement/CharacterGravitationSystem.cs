using System;
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
            var filter = World.Filter.With<CharacterHeadMovingComponent>().With<CharacterMovingComponent>().With<CharacterJumpingComponent>();
            _characterEntity = filter.FirstOrDefault();
        }

        public void OnUpdate(float deltaTime)
        {
            if (_characterEntity == null)
                return;
            
            ref var jumping = ref _characterEntity.GetComponent<CharacterJumpingComponent>();

            if (!_characterEntity.GetComponent<CharacterGroundedComponent>().IsActive)
                jumping.VerticalVelocity += jumping.GravitationalConstant * Time.deltaTime;
        }

        public void Dispose() { }
    }
}
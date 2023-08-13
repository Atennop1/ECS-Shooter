using System;
using Scellecs.Morpeh;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterGroundingSystem : ISystem
    {
        private readonly Transform _characterFeetTransform;
        private readonly LayerMask _groundLayerMask;
        private Entity _characterEntity;
        
        private const float _checkingSphereRadius = 0.475f;

        public CharacterGroundingSystem(Transform characterFeetTransform, LayerMask groundLayerMask)
        {
            _characterFeetTransform = characterFeetTransform ?? throw new ArgumentNullException(nameof(characterFeetTransform));
            _groundLayerMask = groundLayerMask;
        }
        
        public World World { get; set; }
        
        public void OnAwake()
        {
            var filter = World.Filter.With<CharacterJumpingComponent>();
            _characterEntity = filter.FirstOrDefault();
        }
        
        public void OnUpdate(float deltaTime)
        {
            if (_characterEntity == null)
                return;
            
            ref var grounded = ref _characterEntity.GetComponent<CharacterGroundedComponent>();
            grounded.IsActive = UnityEngine.Physics.CheckSphere(_characterFeetTransform.position, _checkingSphereRadius, _groundLayerMask);
        }
        
        public void Dispose() { }
    }
}
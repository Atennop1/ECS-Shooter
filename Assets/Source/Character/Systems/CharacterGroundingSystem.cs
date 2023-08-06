using System;
using Scellecs.Morpeh;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterGroundingSystem : ISystem
    {
        private readonly Transform _characterFeetTransform;
        private readonly LayerMask _groundLayerMask;
        
        private const float _checkingSphereRadius = 0.5f;

        public CharacterGroundingSystem(Transform characterFeetTransform, LayerMask groundLayerMask)
        {
            _characterFeetTransform = characterFeetTransform ?? throw new ArgumentNullException(nameof(characterFeetTransform));
            _groundLayerMask = groundLayerMask;
        }
        
        public World World { get; set; }
        
        public void OnUpdate(float deltaTime)
        {
            var filter = World.Filter.With<CharacterGroundedComponent>();
            var entity = filter.FirstOrDefault();
            
            if (entity != null)
                entity.GetComponent<CharacterGroundedComponent>().IsActive = UnityEngine.Physics.CheckSphere(_characterFeetTransform.position, _checkingSphereRadius, _groundLayerMask);
        }
        
        public void Dispose() { }
        public void OnAwake() { }
    }
}
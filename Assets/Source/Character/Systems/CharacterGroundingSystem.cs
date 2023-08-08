﻿using System;
using Scellecs.Morpeh;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterGroundingSystem : ISystem
    {
        private readonly Transform _characterFeetTransform;
        private readonly LayerMask _groundLayerMask;
        private Entity _characterEntity;
        
        private const float _checkingSphereRadius = 0.5f;

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
            
            if (_characterEntity == null)
                throw new InvalidOperationException("This system can't work without character on scene");
        }
        
        public void OnUpdate(float deltaTime)
        {
            ref var grounded = ref _characterEntity.GetComponent<CharacterGroundedComponent>();
            grounded.IsActive = UnityEngine.Physics.CheckSphere(_characterFeetTransform.position, _checkingSphereRadius, _groundLayerMask);
        }
        
        public void Dispose() { }
    }
}
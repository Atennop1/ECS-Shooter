using System;
using JetBrains.Annotations;
using Scellecs.Morpeh;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterGroundingSystem : ISystem
    {
        private readonly CharacterController _characterController;
        private readonly LayerMask _groundLayerMask;
        private Entity _characterEntity;
        
        private const float _checkingSphereRadius = 0.45f;
        private const float _characterRadiusDecreasingCoefficient = 0.5f;

        public CharacterGroundingSystem(CharacterController characterController, LayerMask groundLayerMask)
        {
            _characterController = characterController ?? throw new ArgumentNullException(nameof(characterController));
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
            var checkingPosition =  _characterController.transform.position - new Vector3(0, _characterController.height / 2 - _characterController.radius * _characterRadiusDecreasingCoefficient, 0);
            grounded.IsActive = UnityEngine.Physics.CheckSphere(checkingPosition, _checkingSphereRadius, _groundLayerMask);
        }
        
        public void Dispose() { }
    }
}
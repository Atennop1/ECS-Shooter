using System;
using Scellecs.Morpeh;
using UnityEngine;
using Zenject;

namespace Shooter.Character
{
    public sealed class CharacterSlidingSystem : ISystem
    {
        private readonly LayerMask _layerMask = LayerMask.GetMask($"Ground");
        
        private CharacterController _characterController;
        private Entity _characterEntity;
        
        [Inject]
        public void Construct(CharacterController characterController) 
            => _characterController = characterController ?? throw new ArgumentNullException(nameof(characterController));

        public World World { get; set; }
        
        public void OnAwake()
        {
            var filter = World.Filter.With<CharacterSlidingComponent>();
            _characterEntity = filter.FirstOrDefault();
        }

        public void OnUpdate(float deltaTime)
        {
            if (_characterEntity == null)
                return;
            
            ref var sliding = ref _characterEntity.GetComponent<CharacterSlidingComponent>();
            sliding.IsActive = false;

            var difference = new Vector3(0, _characterController.height / 2 - _characterController.radius, 0);
            var origin = _characterController.transform.position - difference;
            var radius = _characterController.radius + 0.04f;
            
            if (!UnityEngine.Physics.SphereCast(origin, radius, Vector3.down, out var sphereHit, 1f, _layerMask, QueryTriggerInteraction.Ignore))
                return;
                
            var raycastOrigin = new Vector3(sphereHit.point.x, sphereHit.point.y + 1f, sphereHit.point.z);
            
            if (!UnityEngine.Physics.Raycast(raycastOrigin, Vector3.down, out var raycastHit, 100, _layerMask))
                return;
            
            var hitPointNormal = raycastHit.normal;
            var angle = Vector3.Angle(hitPointNormal, Vector3.up);
            sliding.IsActive = angle > _characterController.slopeLimit;
            
            if (sliding.IsActive)
                _characterController.Move(new Vector3(hitPointNormal.x, -hitPointNormal.y * 3, hitPointNormal.z) * sliding.SlideSpeed * Time.deltaTime * (angle / 45));
        }

        public void Dispose() { }
    }
}
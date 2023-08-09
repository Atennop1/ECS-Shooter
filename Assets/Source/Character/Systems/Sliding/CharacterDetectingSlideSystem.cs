using Scellecs.Morpeh;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterDetectingSlideSystem : ISystem
    {
        private readonly CharacterController _characterController;

        private Entity _characterEntity;
        private Filter _collisionStayFilter;

        public CharacterDetectingSlideSystem(CharacterController characterController)
            => _characterController = characterController;

        public World World { get; set; }

        public void OnAwake()
        {
            var characterFilter = World.Filter.With<CharacterMovingComponent>();
            _collisionStayFilter = World.Filter.With<CharacterOnCollisionStay>();
            _characterEntity = characterFilter.FirstOrDefault();
        }

        public void OnUpdate(float deltaTime)
        {
            if (_characterEntity == null)
                return;
            
            ref var sliding = ref _characterEntity.GetComponent<CharacterSlidingComponent>();
            sliding.IsActive = false;

            foreach (var collisionStayEntity in _collisionStayFilter)
            {
                ref var collisionStay = ref collisionStayEntity.GetComponent<CharacterOnCollisionStay>();

                Debug.Log("Update");
                if (collisionStay.OriginGameObject.GetComponentInParent<CharacterController>() == null)
                    continue;

                Debug.Log("Collision stayed");
                var collisionPoint = collisionStay.Hit.point;
                var collisionDirection = -collisionStay.Hit.normal;

                if (collisionStay.Hit.collider.Raycast(new Ray(collisionPoint - collisionDirection, collisionDirection), out var raycastHit, 2))
                {
                    Debug.Log("Need to slide");
                    sliding.IsActive = Vector3.Angle(Vector3.up, raycastHit.normal) > _characterController.slopeLimit;
                    sliding.SlidingSurfaceNormal = raycastHit.normal;
                }

                Debug.Log("Deleted");
                World.RemoveEntity(collisionStayEntity);
            }
        }

        public void Dispose() { }
    }
}
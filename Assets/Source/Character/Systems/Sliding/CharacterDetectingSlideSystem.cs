using Scellecs.Morpeh;
using Shooter.Physics;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterDetectingSlideSystem : ISystem
    {
        private readonly CharacterController _characterController;

        public CharacterDetectingSlideSystem(CharacterController characterController)
            => _characterController = characterController;

        public World World { get; set; }

        public void OnUpdate(float deltaTime)
        {
            var slidingFilter = World.Filter.With<CharacterSlidingComponent>();
            var slidingEntity = slidingFilter.FirstOrDefault();

            if (slidingEntity == null)
                return;

            ref var sliding = ref slidingEntity.GetComponent<CharacterSlidingComponent>();
            sliding.IsActive = false;

            var collisionStayFilter = World.Filter.With<OnCollisionStay>();
            foreach (var collisionStayEntity in collisionStayFilter)
            {
                ref var collisionStay = ref collisionStayEntity.GetComponent<OnCollisionStay>();

                Debug.Log("Update");
                if (collisionStay.OriginGameObject.GetComponentInParent<CharacterController>() == null)
                    continue;

                Debug.Log("Collision stayed");
                var collisionPoint = collisionStay.Collision.contacts[0].point;
                var collisionDirection = -collisionStay.Collision.contacts[0].normal;

                if (collisionStay.Collision.collider.Raycast(new Ray(collisionPoint - collisionDirection, collisionDirection), out var raycastHit, 2))
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
        public void OnAwake() { }
    }
}
using Leopotam.EcsLite;
using Shooter.Physics;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterDetectingSlideSystem : IEcsRunSystem
    {
        private readonly CharacterController _characterController;

        public CharacterDetectingSlideSystem(CharacterController characterController)
            => _characterController = characterController;
        
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            
            var characterPool = world.GetPool<Character>();
            var characterFilter = world.Filter<Character>().End();

            var collisionStayPool = world.GetPool<OnCollisionStay>();
            var collisionStayFilter = world.Filter<OnCollisionStay>().End();

            foreach (var characterEntity in characterFilter)
            {
                ref var character = ref characterPool.Get(characterEntity);
                character.SlidingData.IsSliding = false;
                
                foreach (var collisionStayEntity in collisionStayFilter)
                {
                    ref var collisionStay = ref collisionStayPool.Get(collisionStayEntity);

                    Debug.Log("Update");
                    if (collisionStay.OriginGameObject.GetComponentInParent<CharacterController>() == null) 
                        continue;
                    
                    Debug.Log("Collision stayed");
                    var collisionPoint = collisionStay.Collision.contacts[0].point;
                    var collisionDirection = -collisionStay.Collision.contacts[0].normal;
                        
                    if (collisionStay.Collision.collider.Raycast(new Ray(collisionPoint - collisionDirection, collisionDirection), out var raycastHit, 2))
                    {
                        Debug.Log("Need to slide");
                        character.SlidingData.IsSliding = Vector3.Angle(Vector3.up, raycastHit.normal) > _characterController.slopeLimit;
                        character.SlidingData.SlidingSurfaceNormal = raycastHit.normal;
                    }
                        
                    Debug.Log("Deleted");
                    collisionStayPool.Del(collisionStayEntity);
                }
            }
        }
    }
}
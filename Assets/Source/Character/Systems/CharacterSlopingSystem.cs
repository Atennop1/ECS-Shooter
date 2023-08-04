using System;
using Leopotam.EcsLite;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterSlopingSystem : IEcsRunSystem
    {
        private readonly CharacterController _characterController;
        private readonly LayerMask _layerMask = LayerMask.GetMask($"Ground");
        
        public CharacterSlopingSystem(CharacterController characterController) 
            => _characterController = characterController ?? throw new ArgumentNullException(nameof(characterController));

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var pool = world.GetPool<Character>();
            var filter = world.Filter<Character>().End();

            foreach (var entity in filter)
            {
                ref var character = ref pool.Get(entity);

                Debug.Log("Update");
                if (!character.IsGrounded) 
                    continue;

                var sphereCastOffsetY = _characterController.height / 2 - _characterController.radius;
                var sphereCastPosition = _characterController.transform.position - new Vector3(0, sphereCastOffsetY, 0);

                Debug.Log("Grounded");
                if (!Physics.SphereCast(sphereCastPosition, _characterController.radius - 0.01f, Vector3.down, out var sphereHit, 0.05f, _layerMask, QueryTriggerInteraction.Ignore))
                    continue;

                Debug.Log(sphereHit.collider.gameObject.name);
                character.MovingData.IsSliding = Vector3.Angle(Vector3.up, sphereHit.normal) > _characterController.slopeLimit;
                
                if (!character.MovingData.IsSliding)
                    continue;

                Debug.Log("Sliding");
                _characterController.Move(new Vector3(sphereHit.normal.x, -sphereHit.normal.y, sphereHit.normal.z) * character.MovingData.SlopSpeed * Time.deltaTime);
            }
        }
    }
}
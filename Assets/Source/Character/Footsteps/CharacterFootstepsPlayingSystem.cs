using System;
using Scellecs.Morpeh;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Shooter.Character
{
    public sealed class CharacterFootstepsPlayingSystem : ISystem
    {
        private readonly Transform _characterTransform;
        private readonly AudioSource _footstepsAudioSource;
        
        private Entity _characterEntity;
        private float _timer;
        
        public CharacterFootstepsPlayingSystem(Transform characterTransform, AudioSource footstepsAudioSource)
        {
            _characterTransform = characterTransform ?? throw new ArgumentNullException(nameof(characterTransform));
            _footstepsAudioSource = footstepsAudioSource ?? throw new ArgumentNullException(nameof(footstepsAudioSource));
        }

        public World World { get; set; }

        public void OnAwake()
        {
            var filter = World.Filter.With<CharacterGroundedComponent>().With<CharacterFootstepsComponent>().With<CharacterCrouchingComponent>().With<CharacterMovingComponent>().With<CharacterSprintingComponent>();
            _characterEntity = filter.FirstOrDefault();
        }
        
        public void OnUpdate(float deltaTime)
        {
            if (_characterEntity == null)
                return;

            ref var grounded = ref _characterEntity.GetComponent<CharacterGroundedComponent>();
            ref var footsteps = ref _characterEntity.GetComponent<CharacterFootstepsComponent>();
            ref var crouching = ref _characterEntity.GetComponent<CharacterCrouchingComponent>();
            ref var moving = ref _characterEntity.GetComponent<CharacterMovingComponent>();
            ref var sprinting = ref _characterEntity.GetComponent<CharacterSprintingComponent>();

            if (!grounded.IsActive || (!moving.IsWalking && !sprinting.IsActive))
                return;

            _timer -= deltaTime;
            
            if (_timer > 0 || !UnityEngine.Physics.Raycast(_characterTransform.position, Vector3.down, out var hit, 3))
                return;
            
            if (!hit.collider.gameObject.TryGetComponent(out IObjectWithFootsteps objectWithFootsteps))
                return;
            
            _footstepsAudioSource.pitch = Random.Range(0.9f, 1.1f);
            _footstepsAudioSource.PlayOneShot(objectWithFootsteps.FootstepsClips[Random.Range(0, objectWithFootsteps.FootstepsClips.Length - 1)]);
            _timer = crouching.IsActive ? footsteps.CrouchingStepTime : (sprinting.IsActive ? footsteps.SprintingStepTime : footsteps.WalkingStepTime);
        }
        
        public void Dispose() { }
    }
}
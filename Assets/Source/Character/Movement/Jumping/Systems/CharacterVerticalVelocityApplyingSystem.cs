using System;
using Scellecs.Morpeh;
using UnityEngine;
using Zenject;

namespace Shooter.Character
{
    public sealed class CharacterVerticalVelocityApplyingSystem : ISystem
    {
        private CharacterController _characterController;
        private Entity _characterEntity;

        [Inject]
        public void Construct(CharacterController characterController) 
            => _characterController = characterController ?? throw new ArgumentNullException(nameof(characterController));

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
            
            ref var jumping = ref _characterEntity.GetComponent<CharacterJumpingComponent>(); 
            _characterController.Move(new Vector3(0, jumping.VerticalVelocity, 0) * Time.deltaTime);
        }

        public void Dispose() { }
    }
}
﻿using Scellecs.Morpeh;

namespace Shooter.Character
{
    public sealed class CharacterStaminaUsingSystem : ISystem
    {
        private Entity _characterEntity;
        
        public World World { get; set; }

        public void OnAwake()
        {
            var filter = World.Filter.With<CharacterStaminaComponent>().With<CharacterSprintingComponent>();
            _characterEntity = filter.FirstOrDefault();
        }
        
        public void OnUpdate(float deltaTime)
        {
            if (_characterEntity == null)
                return;

            ref var sprinting = ref _characterEntity.GetComponent<CharacterSprintingComponent>();
            ref var stamina = ref _characterEntity.GetComponent<CharacterStaminaComponent>();
            
            if (stamina.CurrentValue <= 0)
                sprinting.CanSprint = false;
            
            if (!sprinting.IsActive)
                return;

            stamina.CurrentValue -= stamina.DecreasingPerSecondAmount * deltaTime;

            if (stamina.CurrentValue < 0)
                stamina.CurrentValue = 0;
        }
        
        public void Dispose() { }
    }
}
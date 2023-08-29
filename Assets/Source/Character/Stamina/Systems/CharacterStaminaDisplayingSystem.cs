using System;
using Scellecs.Morpeh;
using UnityEngine.UI;

namespace Shooter.Character
{
    public sealed class CharacterStaminaDisplayingSystem : ISystem
    {
        private readonly Slider _staminaSlider;
        private Entity _characterEntity;

        public CharacterStaminaDisplayingSystem(Slider staminaSlider) 
            => _staminaSlider = staminaSlider ?? throw new ArgumentNullException(nameof(staminaSlider));

        public World World { get; set; }
        
        public void OnAwake()
        {
            var filter = World.Filter.With<CharacterStaminaComponent>();
            _characterEntity = filter.FirstOrDefault();
        }
        
        public void OnUpdate(float deltaTime)
        {
            if (_characterEntity == null)
                return;

            ref var stamina = ref _characterEntity.GetComponent<CharacterStaminaComponent>();
            _staminaSlider.value = stamina.CurrentValue / stamina.MaxValue;
        }
        
        public void Dispose() { }
    }
}
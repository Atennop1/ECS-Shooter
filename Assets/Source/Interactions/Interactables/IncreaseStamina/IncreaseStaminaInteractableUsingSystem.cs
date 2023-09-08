using Scellecs.Morpeh;
using Shooter.Character;

namespace Shooter.Interactions
{
    public sealed class IncreaseStaminaInteractableUsingSystem : ISystem
    {
        private Entity _characterEntity;
        private Filter _interactablesFilter;
        
        public World World { get; set; }

        public void OnAwake()
        {
            _interactablesFilter = World.Filter.With<InteractableComponent>().With<IncreaseStaminaInteractableComponent>().With<InteractableActivatedComponent>();
            var characterFilter = World.Filter.With<CharacterStaminaComponent>();
            _characterEntity = characterFilter.FirstOrDefault();
        }

        public void OnUpdate(float deltaTime)
        {
            if (_characterEntity == null)
                return;
            
            foreach (var entity in _interactablesFilter)
            {
                ref var increaseStaminaInteractable = ref entity.GetComponent<IncreaseStaminaInteractableComponent>();
                ref var characterStamina = ref _characterEntity.GetComponent<CharacterStaminaComponent>();

                characterStamina.CurrentValue += increaseStaminaInteractable.AddingStaminaAmount;

                if (characterStamina.CurrentValue > characterStamina.MaxValue)
                    characterStamina.CurrentValue = characterStamina.MaxValue;
            }
        }
        
        public void Dispose() { }
    }
}
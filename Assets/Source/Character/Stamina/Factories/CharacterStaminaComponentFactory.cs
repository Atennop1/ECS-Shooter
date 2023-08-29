using Scellecs.Morpeh;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterStaminaComponentFactory : SerializedMonoBehaviour
    {
        [SerializeField] private float _maxValue;
        [SerializeField] private float _decreasingPerSecondAmount;

        public void CreateFor(Entity entity)
        {
            ref var createdComponent = ref entity.AddComponent<CharacterStaminaComponent>();
            
            createdComponent.DecreasingPerSecondAmount = _decreasingPerSecondAmount;
            createdComponent.CurrentValue = createdComponent.MaxValue = _maxValue;
        }
    }
}
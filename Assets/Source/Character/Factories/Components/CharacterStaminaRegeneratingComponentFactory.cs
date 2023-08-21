using Scellecs.Morpeh;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterStaminaRegeneratingComponentFactory : SerializedMonoBehaviour
    {
        [SerializeField] private int _timeBeforeRegeneratingInMilliseconds;
        [SerializeField] private float _regeneratingPerSecondAmount;
        
        public void CreateFor(Entity entity)
        {
            ref var createdComponent = ref entity.AddComponent<CharacterStaminaRegeneratingComponent>();
            
            createdComponent.TimeBeforeRegeneratingInMilliseconds = _timeBeforeRegeneratingInMilliseconds;
            createdComponent.RegeneratingPerSecondAmount = _regeneratingPerSecondAmount;
        }
    }
}
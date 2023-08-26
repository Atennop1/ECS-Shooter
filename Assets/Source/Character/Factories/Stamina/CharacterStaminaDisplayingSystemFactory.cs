using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Shooter.Character
{
    public sealed class CharacterStaminaDisplayingSystemFactory : SerializedMonoBehaviour
    {
        [SerializeField] private Slider _staminaSlider;

        public CharacterStaminaDisplayingSystem Create() 
            => new(_staminaSlider);
    }
}
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterSprintingApplyingSystemFactory : MonoBehaviour
    {
        [SerializeField] private float _sprintingSpeed;

        public CharacterSprintingApplyingSystem Create()
            => new(_sprintingSpeed);
    }
}
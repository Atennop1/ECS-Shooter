using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterSprintingSystemFactory : MonoBehaviour
    {
        [SerializeField] private float _sprintingSpeed;

        public CharacterSprintingSystem Create()
            => new(_sprintingSpeed);
    }
}
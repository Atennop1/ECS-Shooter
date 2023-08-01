using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterCameraMovingFactory : MonoBehaviour
    {
        [SerializeField] private float _mouseSensitivity;

        public CharacterCameraMoving Create()
        {
            var createdMoving = new CharacterCameraMoving
            {
                MouseSensitivity = _mouseSensitivity
            };
            
            return createdMoving;
        }
    }
}
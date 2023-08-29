using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterGroundingSystemFactory : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private LayerMask _groundLayerMask;

        public CharacterGroundingSystem Create() 
            => new(_characterController, _groundLayerMask);
    }
}
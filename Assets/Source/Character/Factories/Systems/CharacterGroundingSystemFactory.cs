using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterGroundingSystemFactory : MonoBehaviour
    {
        [SerializeField] private Transform _characterFeetTransform;
        [SerializeField] private LayerMask _groundLayerMask;

        public CharacterGroundingSystem Create() 
            => new(_characterFeetTransform, _groundLayerMask);
    }
}
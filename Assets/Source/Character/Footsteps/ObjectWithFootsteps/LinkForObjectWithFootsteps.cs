using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.Character
{
    public class LinkForObjectWithFootsteps : SerializedMonoBehaviour, IObjectWithFootsteps
    {
        [SerializeField] private IObjectWithFootsteps _objectWithFootsteps;

        public AudioClip[] FootstepsClips 
            => _objectWithFootsteps.FootstepsClips;
    }
}
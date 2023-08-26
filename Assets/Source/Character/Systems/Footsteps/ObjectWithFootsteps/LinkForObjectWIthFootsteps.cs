using UnityEngine;

namespace Shooter.Character
{
    public class LinkForObjectWIthFootsteps : MonoBehaviour, IObjectWithFootsteps
    {
        [SerializeField] private IObjectWithFootsteps _objectWithFootsteps;

        public AudioClip[] FootstepsClips 
            => _objectWithFootsteps.FootstepsClips;
    }
}
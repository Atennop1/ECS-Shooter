using UnityEngine;

namespace Shooter.Character
{
    public class ObjectWithFootsteps : MonoBehaviour, IObjectWithFootsteps
    {
        [field: SerializeField] public AudioClip[] FootstepsClips { get; private set; }
    }
}
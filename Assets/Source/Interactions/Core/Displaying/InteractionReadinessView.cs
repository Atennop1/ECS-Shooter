using UnityEngine;

namespace Shooter.Interactions
{
    public sealed class InteractionReadinessView : MonoBehaviour
    {
        [SerializeField] private GameObject _readinessTextGameObject;

        public void DisplayReadiness()
            => _readinessTextGameObject.SetActive(true);

        public void DisplayUnreadiness()
            => _readinessTextGameObject.SetActive(false);
    }
}
using TMPro;
using UnityEngine;

namespace Shooter.Interactions
{
    public sealed class InteractionReadinessView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _readinessText;

        public void DisplayReadiness(InteractableComponent interactableComponent)
        {
            _readinessText.text = interactableComponent.PromptMessage;
            _readinessText.gameObject.SetActive(true);
        }

        public void DisplayUnreadiness()
            => _readinessText.gameObject.SetActive(false);
    }
}
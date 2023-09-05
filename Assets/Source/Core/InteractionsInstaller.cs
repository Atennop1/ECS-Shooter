using System;
using Shooter.Core.GameLoop;
using Shooter.Interactions;
using UnityEngine;
using Zenject;

namespace Shooter.Core
{
    public sealed class InteractionsInstaller : MonoInstaller
    {
        [SerializeField] private Transform _characterHeadTransform;
        [SerializeField] private LayerMask _interactionsLayerMask;
        
        private IGameLoop _gameLoop;

        [Inject]
        public void Construct(IGameLoop gameLoop) 
            => _gameLoop = gameLoop ?? throw new ArgumentNullException(nameof(gameLoop));

        public override void InstallBindings()
        {
            _gameLoop.AddSystem(new InteractionDetectingSystem(_characterHeadTransform, _interactionsLayerMask));
            _gameLoop.AddSystem(new InteractionActivatingSystem());
        }
    }
}
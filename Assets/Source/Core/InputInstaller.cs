using System;
using Scellecs.Morpeh;
using Shooter.Core.GameLoop;
using Shooter.Input;
using Shooter.Input.Aim;
using Zenject;

namespace Shooter.Core
{
    public sealed class InputInstaller : MonoInstaller
    {
        private World _world;
        private IGameLoop _gameLoop;

        [Inject]
        public void Construct(World world, IGameLoop gameLoop)
        {
            _world = world ?? throw new ArgumentNullException(nameof(world));
            _gameLoop = gameLoop ?? throw new ArgumentNullException(nameof(gameLoop));
        }

        public override void InstallBindings()
        {
            var entity = _world.CreateEntity();
            
            entity.AddComponent<MovingInputComponent>();
            entity.AddComponent<SprintingInputComponent>();
            entity.AddComponent<CrouchingInputComponent>();
            entity.AddComponent<JumpingInputComponent>();
            entity.AddComponent<InteractingInputComponent>();
            entity.AddComponent<AimInputComponent>();
            
            _gameLoop.AddSystem(new MovingInputReadingSystem(new CharacterControls()));
            _gameLoop.AddSystem(new SprintingInputReadingSystem());
            _gameLoop.AddSystem(new CrouchingInputReadingSystem());
            _gameLoop.AddSystem(new JumpingInputReadingSystem());
            _gameLoop.AddSystem(new InteractingInputReadingSystem());
            _gameLoop.AddSystem(new AimInputReadingSystem());
        }
    }
}
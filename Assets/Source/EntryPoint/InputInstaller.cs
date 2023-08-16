using System;
using Scellecs.Morpeh;
using Shooter.GameLoop;
using Shooter.Input;
using Zenject;

namespace Shooter.EntryPoint
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
            
            entity.AddComponent<MovementInputComponent>();
            entity.AddComponent<CrouchingInputComponent>();
            entity.AddComponent<JumpingInputComponent>();
            
            _gameLoop.AddSystem(new MovementInputReadingSystem(new CharacterControls()));
            _gameLoop.AddSystem(new CrouchingInputReadingSystem(new CharacterControls()));
            _gameLoop.AddSystem(new JumpingInputReadingSystem(new CharacterControls())); }
    }
}
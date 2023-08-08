using Scellecs.Morpeh;
using Shooter.GameLoop;
using Shooter.Input;
using Zenject;

namespace Shooter.EntryPoint
{
    public sealed class InputInstaller : MonoInstaller
    {
        [Inject] private World _world;
        [Inject] private IGameLoop _gameLoop;

        public override void InstallBindings()
        {
            var entity = _world.CreateEntity();
            entity.AddComponent<PlayerInputComponent>();
            _gameLoop.AddSystem(new PlayerInputReadingSystem(new CharacterControls()));
        }
    }
}
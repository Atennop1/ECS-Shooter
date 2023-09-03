using Scellecs.Morpeh;
using Zenject;

namespace Shooter.Core
{
    public sealed class EcsInstaller : MonoInstaller
    {
        private GameLoop.GameLoop _gameLoop;
        
        public override void InstallBindings()
        {
            var world = World.Create();
            _gameLoop = new GameLoop.GameLoop(world);

            Container.BindInstance(world).AsSingle();
            Container.BindInterfacesAndSelfTo<GameLoop.GameLoop>().FromInstance(_gameLoop).AsSingle();
        }

        private void OnDestroy()
            => _gameLoop.Dispose();
    }
}
using Scellecs.Morpeh;

namespace Shooter.Core.GameLoop
{
    public interface IGameLoop
    {
        void AddInitializer(IInitializer initializer);
        void AddSystem(ISystem system);
    }
}
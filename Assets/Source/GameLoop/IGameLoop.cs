using Scellecs.Morpeh;

namespace Shooter.GameLoop
{
    public interface IGameLoop
    {
        void AddInitializer(IInitializer initializer);
        void AddSystem(ISystem system);
    }
}
using Scellecs.Morpeh.Providers;
using Unity.IL2CPP.CompilerServices;

namespace Shooter.Character
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, true)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class FootstepsPlayingProvider : MonoProvider<FootstepsPlayingComponent> { }
}
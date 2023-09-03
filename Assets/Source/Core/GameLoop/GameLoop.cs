using System;
using Scellecs.Morpeh;
using UnityEngine;
using Zenject;

namespace Shooter.Core.GameLoop
{
    public sealed class GameLoop : IGameLoop, IInitializable, ITickable, IFixedTickable, ILateTickable
    {
        private readonly SystemsGroup _systemsGroup;
        private readonly World _world;

        public GameLoop(World world)
        {
            _world = world ?? throw new ArgumentNullException(nameof(world));
            _systemsGroup = _world.CreateSystemsGroup();
        }

        public void AddInitializer(IInitializer initializer) 
            => _systemsGroup.AddInitializer(initializer);

        public void AddSystem(ISystem system) 
            => _systemsGroup.AddSystem(system);

        public void Initialize() 
            => _systemsGroup.Initialize();

        public void Tick() 
            => _systemsGroup.Update(Time.deltaTime);

        public void FixedTick() 
            => _systemsGroup.FixedUpdate(Time.fixedDeltaTime);

        public void LateTick() 
            => _systemsGroup.LateUpdate(Time.deltaTime);

        public void Dispose()
        {
            _systemsGroup.Dispose();
            _world.Dispose();
        }
    }
}
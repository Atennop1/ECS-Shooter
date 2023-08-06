using System;
using Scellecs.Morpeh;
using Shooter.Character;
using Shooter.Input;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.EntryPoint
{
    public sealed class Game : SerializedMonoBehaviour
    {
        [SerializeField] private PlayerInputFactory _playerInputFactory;
        [SerializeField] private CharacterFactory _characterFactory;

        private World _world;
        
        private void Awake()
        {
            _world = World.Create();
            _world.UpdateByUnity = false;
            var systemsGroup = _world.CreateSystemsGroup();

            _playerInputFactory.Create(_world, systemsGroup);
            _characterFactory.Create(_world, systemsGroup);
        }
        
        private void Update()
        {
            _world.Update(Time.deltaTime);
            _world.CleanupUpdate(Time.deltaTime);
            _world.Commit();
        }

        private void LateUpdate()
            => _world.LateUpdate(Time.deltaTime);

        private void FixedUpdate()
            => _world.FixedUpdate(Time.fixedDeltaTime);
    }
}
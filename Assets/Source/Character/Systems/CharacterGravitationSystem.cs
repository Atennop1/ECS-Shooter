﻿using Leopotam.EcsLite;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterGravitationSystem : IEcsRunSystem
    {
        private readonly CharacterController _characterController;

        public CharacterGravitationSystem(CharacterController characterController)
            => _characterController = characterController;
        
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var pool = world.GetPool<Character>();
            var filter = world.Filter<Character>().End();

            foreach (var entity in filter)
            {
                ref var character = ref pool.Get(entity);

                if (character.Jumping.VerticalVelocity < 0 && character.IsGrounded) 
                    character.Jumping.VerticalVelocity = -2f;

                character.Jumping.VerticalVelocity += character.Jumping.GravitationalConstant * Time.deltaTime;
                _characterController.Move(new Vector3(0, character.Jumping.VerticalVelocity, 0) * Time.deltaTime);
            }
        }
    }
}
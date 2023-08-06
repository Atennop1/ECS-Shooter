using Scellecs.Morpeh;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterGravitationSystem : ISystem
    {
        public World World { get; set; }

        public void OnUpdate(float deltaTime)
        {
            var filter = World.Filter.With<CharacterJumpingComponent>().With<CharacterGroundedComponent>();
            var entity = filter.FirstOrDefault();

            if (entity == null)
                return;

            ref var jumping = ref entity.GetComponent<CharacterJumpingComponent>();

            if (!entity.GetComponent<CharacterGroundedComponent>().IsActive)
                jumping.VerticalVelocity += jumping.GravitationalConstant * Time.deltaTime;

        }

        public void Dispose() { }
        public void OnAwake() { }
    }
}
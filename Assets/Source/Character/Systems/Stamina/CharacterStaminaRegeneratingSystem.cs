using System.Threading;
using Cysharp.Threading.Tasks;
using Scellecs.Morpeh;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterStaminaRegeneratingSystem : ISystem
    {
        private Entity _characterEntity;
        
        private float _lastStaminaValue;
        private CancellationTokenSource _cancellationTokenSource = new();
        
        public World World { get; set; }
        
        public void OnAwake()
        {
            var filter = World.Filter.With<CharacterStaminaRegeneratingComponent>();
            _characterEntity = filter.FirstOrDefault();

            if (_characterEntity != null)
                _lastStaminaValue = _characterEntity.GetComponent<CharacterStaminaComponent>().CurrentValue;
        }
        
        public async void OnUpdate(float deltaTime)
        {
            if (_characterEntity == null)
                return;

            var stamina = _characterEntity.GetComponent<CharacterStaminaComponent>();

            if (_lastStaminaValue > stamina.CurrentValue)
            {
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource = new CancellationTokenSource();
                await RegenerateStamina(_cancellationTokenSource.Token);
            }

            _lastStaminaValue = stamina.CurrentValue;
        }
        
        public void Dispose() { }

        private async UniTask RegenerateStamina(CancellationToken cancellationToken)
        {
            var regeneratingStamina = _characterEntity.GetComponent<CharacterStaminaRegeneratingComponent>();
            var stamina = _characterEntity.GetComponent<CharacterStaminaComponent>();
            
            await UniTask.Delay(regeneratingStamina.TimeBeforeRegeneratingInMilliseconds, cancellationToken: cancellationToken);

            while (stamina.CurrentValue < stamina.MaxValue)
            {
                if (cancellationToken.IsCancellationRequested)
                    return;

                _characterEntity.GetComponent<CharacterStaminaComponent>().CurrentValue += regeneratingStamina.RegeneratingPerSecondAmount * Time.deltaTime;
                await UniTask.Yield();
            }
        }
    }
}
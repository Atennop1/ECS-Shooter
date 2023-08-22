using System.Threading;
using System.Threading.Tasks;
using Scellecs.Morpeh;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterStaminaRegeneratingSystem : ISystem
    {
        private Entity _characterEntity;
        
        private float _lastStaminaValue;
        private CancellationTokenSource _cancellationTokenSource;

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
                _cancellationTokenSource?.Cancel();
                _cancellationTokenSource = new CancellationTokenSource();
                
                try { await RegenerateStamina(_cancellationTokenSource.Token); }
                catch { /*IGNORED*/ }
            }

            _lastStaminaValue = stamina.CurrentValue;
        }

        public void Dispose() 
            => _cancellationTokenSource?.Dispose();

        private async Task RegenerateStamina(CancellationToken cancellationToken)
        {
            var regeneratingStamina = _characterEntity.GetComponent<CharacterStaminaRegeneratingComponent>();
            var stamina = _characterEntity.GetComponent<CharacterStaminaComponent>();
            
            await Task.Delay(regeneratingStamina.TimeBeforeRegeneratingInMilliseconds, cancellationToken: cancellationToken); 
            
            while (_characterEntity.GetComponent<CharacterStaminaComponent>().CurrentValue < stamina.MaxValue)
            {
                cancellationToken.ThrowIfCancellationRequested();
                _characterEntity.GetComponent<CharacterStaminaComponent>().CurrentValue += regeneratingStamina.RegeneratingPerSecondAmount * Time.deltaTime;

                if (_characterEntity.GetComponent<CharacterStaminaComponent>().CurrentValue > stamina.MaxValue)
                    _characterEntity.GetComponent<CharacterStaminaComponent>().CurrentValue = stamina.MaxValue;

                await Task.Delay((int)(Time.deltaTime * 1000));
            }
        }
    }
}
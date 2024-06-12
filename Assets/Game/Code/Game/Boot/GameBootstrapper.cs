using VContainer.Unity;
using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Fusion;
using Game.Code.Common.StateMachineBase;
using Game.Code.Game.Core;
using Game.Code.Game.Core.States;
using Game.Code.Game.Services;
using VContainer;

namespace Game.Code.Game.Boot
{
    public class GameBootstrapper : IAsyncStartable, IDisposable
    {
        private readonly GameStateMachine _stateMachine;
        private readonly StateFactory _stateFactory;
        
        private readonly NetworkFacade _networkFacade;
        private readonly NetworkRunner _networkRunner;


        public GameBootstrapper(NetworkMonoServiceLocator networkServiceLocator, GameStateMachine stateMachine, NetworkFacade networkFacade, 
            StateFactory stateFactory)
        {
            _stateMachine = stateMachine;
            _stateFactory = stateFactory;

            _networkFacade = networkFacade;
            _networkRunner = networkServiceLocator.Runner;
        }


        public async UniTask StartAsync(CancellationToken cancellation)
        {
            _networkRunner.AddCallbacks(_networkFacade);

            _stateMachine.RegisterState(_stateFactory.Create<GameNetworkBootstrapState>(Lifetime.Scoped));
            _stateMachine.RegisterState(_stateFactory.Create<BootstrapState>(Lifetime.Scoped));
            _stateMachine.RegisterState(_stateFactory.Create<LobbyState>(Lifetime.Scoped));
            _stateMachine.RegisterState(_stateFactory.Create<GameState>(Lifetime.Scoped));

            await _stateMachine.Enter<GameNetworkBootstrapState>();
        }

        public void Dispose() =>
            _networkRunner.RemoveCallbacks(_networkFacade);
    }
}
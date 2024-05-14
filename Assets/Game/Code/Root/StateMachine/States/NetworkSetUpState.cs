using Game.Code.Common.StateMachineBase.Interfaces;
using Game.Code.Infrastructure.SceneManaging;
using Cysharp.Threading.Tasks;

namespace Game.Code.Root.StateMachine.States
{
    public class NetworkSetUpState : IState
    {
        private readonly RootStateMachine _stateMachine;

        private readonly TransitionHandler _transitionHandler;
        private readonly SceneLoader _sceneLoader;

        public NetworkSetUpState(RootStateMachine stateMachine, TransitionHandler transitionHandler, SceneLoader sceneLoader)
        {
            _transitionHandler = transitionHandler;

            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }


        public async UniTask Enter()
        {
            await _transitionHandler.PlayFadeInAnimation();

            await _sceneLoader.Load(Scenes.Game);
            await _stateMachine.Enter<GameState>();
        }

        public UniTask Exit() =>
            UniTask.CompletedTask;
    }
}
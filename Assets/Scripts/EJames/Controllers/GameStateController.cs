#region

using System.Collections.Generic;
using EJames.GameStates;
using EJames.Utility;
using Zenject;

#endregion

namespace EJames.Controllers
{
    public class GameStateController : IInitable
    {
        [Inject]
        private Instantiator _instantiator;

        private IGameState _currentState;

        private Dictionary<GameState, IGameState> _gameStatesMap = new Dictionary<GameState, IGameState>();

        public GameState State { get; private set; }

        public void SetState(GameState gameState)
        {
            State = gameState;

            _currentState?.OnExit();
            _gameStatesMap.TryGetValue(State, out _currentState);
            _currentState?.OnEnter();
        }

        void IInitable.Init()
        {
            _gameStatesMap.Add(GameState.Lobby, _instantiator.Instantiate<GameStateLobby>());
            _gameStatesMap.Add(GameState.Auction, _instantiator.Instantiate<GameStateAuction>());
            _gameStatesMap.Add(GameState.Gameplay, _instantiator.Instantiate<GameStateGameplay>());

            SetState(GameState.Lobby);
        }
    }
}
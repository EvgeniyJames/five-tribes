#region

using EJames.Controllers;
using EJames.Models;
using Zenject;

#endregion

namespace EJames.GameStates
{
    public class GameStateGameplay : IGameState
    {
        [Inject]
        private PlayersController _playersController;

        [Inject]
        private PlayerOrderController _playerOrderController;

        void IGameState.OnEnter()
        {
            for (int i = 0; i < _playersController.Players.Count; i++)
            {
                Player player = _playersController.Players[i];
                _playerOrderController.SetPlayerOrder(player, i);
            }
        }

        void IGameState.OnExit()
        {
        }
    }
}
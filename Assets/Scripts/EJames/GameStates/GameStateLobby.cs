#region

using EJames.Controllers;
using EJames.Popups;
using Zenject;

#endregion

namespace EJames.GameStates
{
    public class GameStateLobby : IGameState
    {
        [Inject]
        private PopupsController _popupsController;

        void IGameState.OnEnter()
        {
            _popupsController.ShowPopup<PopupLobby>();
        }

        void IGameState.OnExit()
        {
            _popupsController.HidePopup<PopupLobby>();
        }
    }
}
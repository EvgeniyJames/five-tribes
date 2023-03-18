#region

using EJames.Controllers;
using EJames.Models;
using UnityEngine;
using Zenject;

#endregion

namespace EJames.GameStates
{
    public class GameStateGameplay : IGameState
    {
        [Inject]
        private PlayersAuctionController _playersAuctionController;

        void IGameState.OnEnter()
        {
            for (int i = _playersAuctionController.AuctionSlots.Count - 1; i >= 0; i--)
            {
                AuctionSlot auctionSlot = _playersAuctionController.AuctionSlots[i];
                foreach (Player player in auctionSlot.Players)
                {
                    Debug.Log(player.Id);
                }
            }
        }

        void IGameState.OnExit()
        {
        }
    }
}
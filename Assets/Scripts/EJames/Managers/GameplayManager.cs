using EJames.Utility;
using UnityEngine;

namespace EJames.Managers
{
    public class GameplayManager : SingletonNetwork<GameplayManager>
    {
        public void ServerSceneInit(ulong clientId)
        {
            Debug.Log($"GameplayManager::ServerSceneInit: {clientId}");
        }
    }
}
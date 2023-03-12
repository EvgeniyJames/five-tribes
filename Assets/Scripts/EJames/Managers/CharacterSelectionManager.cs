
using EJames.Utility;
using UnityEngine;

namespace EJames.Managers
{
    public class CharacterSelectionManager : SingletonNetwork<CharacterSelectionManager>
    {
        [SerializeField] private GameObject _playerPrefab;

        public void ServerSceneInit(ulong clientId)
        {
            //GameObject go =
            //    NetworkObjectSpawner.SpawnNewNetworkObjectChangeOwnershipToClient(
            //        _playerPrefab,
            //        transform.position,
            //        clientId);
        }
    }
}
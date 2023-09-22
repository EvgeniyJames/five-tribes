#region

using EJames.Managers;
using Unity.Netcode;
using UnityEngine;

#endregion

namespace EJames.Helpers
{
    public class NetworkActions : MonoBehaviour
    {
        [SerializeField]
        private SceneName _lobbyScene;

        public void OnStartHost()
        {
            NetworkManager.Singleton.StartHost();
            LoadingSceneManager.Instance.LoadScene(_lobbyScene);
        }

        public void OnStartClient()
        {
            NetworkManager.Singleton.StartClient();
        }

        public void OnOffline()
        {
        }
    }
}
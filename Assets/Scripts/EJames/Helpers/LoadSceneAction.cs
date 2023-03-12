#region

using EJames.Managers;
using Unity.Netcode;
using UnityEngine;

#endregion

namespace EJames.Helpers
{
    public class LoadSceneAction : NetworkBehaviour
    {
        [SerializeField]
        private SceneName _sceneName;

        public void OnLoadScene()
        {
            LoadNexSceneServerRpc();
        }

        [ServerRpc]
        private void LoadNexSceneServerRpc()
        {
            LoadingSceneManager.Instance.LoadScene(_sceneName);
        }
    }
}
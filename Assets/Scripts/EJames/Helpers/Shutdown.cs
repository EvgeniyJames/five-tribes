#region

using EJames.Managers;
using Unity.Netcode;
using UnityEngine;

#endregion

namespace EJames.Helpers
{
    public class Shutdown : MonoBehaviour
    {
        public void OnShutdown()
        {
            NetworkManager.Singleton.Shutdown();
            LoadingSceneManager.Instance.LoadScene(SceneName.MainMenu, false);
        }
    }
}
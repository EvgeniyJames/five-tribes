#region

using System;
using EJames.Utility;
using Unity.Netcode;
using UnityEngine.SceneManagement;

#endregion

namespace EJames.Managers
{
    public enum SceneName : byte
    {
        EntryPoint,
        Gameplay,
        Lobby,
        MainMenu,
    };

    public class LoadingSceneManager : SingletonPersistent<LoadingSceneManager>
    {
        private SceneName _sceneActive;

        public SceneName SceneActive => _sceneActive;

        public void Init()
        {
            NetworkManager.Singleton.SceneManager.OnLoadComplete -= OnLoadComplete;
            NetworkManager.Singleton.SceneManager.OnLoadComplete += OnLoadComplete;
        }

        public void LoadScene(SceneName sceneToLoad, bool isNetworkSessionActive = true)
        {
            Loading(sceneToLoad, isNetworkSessionActive);
        }

        private void Loading(SceneName sceneToLoad, bool isNetworkSessionActive)
        {
            if (isNetworkSessionActive)
            {
                if (NetworkManager.Singleton.IsServer)
                {
                    LoadSceneNetwork(sceneToLoad);
                }
            }
            else
            {
                LoadSceneLocal(sceneToLoad);
            }
        }

        private void LoadSceneLocal(SceneName sceneToLoad)
        {
            SceneManager.LoadScene(sceneToLoad.ToString());
        }

        private void LoadSceneNetwork(SceneName sceneToLoad)
        {
            NetworkManager.Singleton.SceneManager.LoadScene(
                sceneToLoad.ToString(),
                LoadSceneMode.Single);
        }

        private void OnLoadComplete(ulong clientId, string sceneName, LoadSceneMode loadSceneMode)
        {
            if (NetworkManager.Singleton.IsServer)
            {
                Enum.TryParse(sceneName, out _sceneActive);
                switch (_sceneActive)
                {
                    case SceneName.Lobby:
                        CharacterSelectionManager.Instance.ServerSceneInit(clientId);
                        break;

                    case SceneName.Gameplay:
                        GameplayManager.Instance.ServerSceneInit(clientId);
                        break;
                }
            }
        }
    }
}
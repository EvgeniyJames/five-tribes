#region

using System.Collections;
using Unity.Netcode;
using UnityEngine;

#endregion

namespace EJames.Managers
{
    public class MenuManager : MonoBehaviour
    {
        private IEnumerator Start()
        {
            yield return new WaitUntil(() => NetworkManager.Singleton.SceneManager != null);
            LoadingSceneManager.Instance.Init();
        }
    }
}
#region

using System.Collections;
using EJames.Managers;
using UnityEngine;

#endregion

namespace EJames.Helpers
{
    public class GoToMenu : MonoBehaviour
    {
        protected IEnumerator Start()
        {
            yield return new WaitUntil(() => LoadingSceneManager.Instance != null);
            LoadingSceneManager.Instance.LoadScene(SceneName.MainMenu, false);
        }
    }
}
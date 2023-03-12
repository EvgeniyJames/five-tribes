#region

using EJames.Controllers;
using UnityEngine;
using Zenject;

#endregion

namespace EJames.Popups
{
    public class BasePopup : MonoBehaviour, IPopup
    {
        [Inject]
        private PopupsController _popupsController;

        void IPopup.Show()
        {
            gameObject.SetActive(true);
        }

        void IPopup.Hide()
        {
            gameObject.SetActive(false);
        }

        protected void Awake()
        {
            AwakeInternal();
        }

        protected virtual void AwakeInternal()
        {
            _popupsController.RegisterPopup(this);
        }
    }
}
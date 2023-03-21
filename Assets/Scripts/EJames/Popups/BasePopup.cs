#region

using System.Collections.Generic;
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

        protected Dictionary<string, object> Args { get; private set; }

        void IPopup.Show(Dictionary<string, object> args)
        {
            Args = args;
            ShowInternal();
        }

        void IPopup.Hide()
        {
            HideInternal();
        }

        protected void Awake()
        {
            AwakeInternal();
        }

        protected virtual void AwakeInternal()
        {
            _popupsController.RegisterPopup(this);
        }

        protected virtual void ShowInternal()
        {
            gameObject.SetActive(true);
        }

        protected virtual void HideInternal()
        {
            gameObject.SetActive(false);
        }
    }
}
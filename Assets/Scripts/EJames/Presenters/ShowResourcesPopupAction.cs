using EJames.Controllers;
using EJames.Popups;
using UnityEngine;
using Zenject;

namespace EJames.Presenters
{
    public class ShowResourcesPopupAction : MonoBehaviour
    {
        [Inject]
        private PopupsController _popupsController;

        public void ShowPopup()
        {
            _popupsController.ShowPopup<SelectResourcesPopup>();
        }
    }
}

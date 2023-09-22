using EJames.Controllers;
using UnityEngine;
using Zenject;

namespace EJames.Presenters
{
    public class ResourceCardSelector : MonoBehaviour
    {
        [SerializeField]
        private ResourceCardPresenter _presenter;

        [Inject]
        private ResourceSelectionController _resourceSelectionController;


        public void OnSelect()
        {
            _resourceSelectionController.ProcessedResource(_presenter.Resource);
        }
    }
}
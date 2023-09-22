#region

using EJames.Controllers;
using EJames.Models;
using EJames.Presenters;
using Zenject;

#endregion

namespace EJames.Popups
{
    public class SelectResourcesPopup : BasePopup
    {
        [Inject]
        private ResourcesDeckPresenter _resourcesDeckPresenter;

        [Inject]
        private ResourceSelectionController _resourceSelectionController;

        [Inject]
        private PopupsController _popupsController;

        public void OnConfirmed()
        {
            _resourceSelectionController.OnDone();
            _popupsController.HidePopup<SelectResourcesPopup>();
        }

        public void OnSelect(Resource resource)
        {
            ResourceCardPresenter presenter = _resourcesDeckPresenter.GetPresenter(resource);
            presenter.Select();
        }

        protected override void ShowInternal()
        {
            base.ShowInternal();

            _resourceSelectionController.Selected += OnSelect;
            _resourceSelectionController.Deselected += OnDeselect;

            _resourceSelectionController.SelectResources(2);
        }

        protected override void HideInternal()
        {
            base.HideInternal();

            _resourceSelectionController.Selected -= OnSelect;
            _resourceSelectionController.Deselected -= OnDeselect;
        }

        private void OnDeselect(Resource resource)
        {
            ResourceCardPresenter presenter = _resourcesDeckPresenter.GetPresenter(resource);
            presenter.Deselect();
        }
    }
}
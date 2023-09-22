using System;
using System.Collections.Generic;
using EJames.Controllers;
using EJames.Models;
using EJames.Utility;
using UnityEngine;
using Zenject;

namespace EJames.Presenters
{
    public class ResourcesDeckPresenter : MonoBehaviour
    {
        [SerializeField]
        private List<ResourceCardItem> _resourceCardPrefabs;

        [SerializeField]
        private List<Transform> _cardParents;

        [Header("Flipped Deck")]
        [SerializeField]
        private Transform _flippedDeckParent;

        [SerializeField]
        private Vector3 _nextCardOffset;

        [Inject]
        private ResourcesManager _resourceManager;

        [Inject]
        private Instantiator _instantiator;

        private List<ResourceCardPresenter> _resourceCardPresenters = new List<ResourceCardPresenter>();
        private List<ResourceCardPresenter> _currentDeckPresenters = new List<ResourceCardPresenter>();

        public List<ResourceCardPresenter> CurrentDeckPresenters => _currentDeckPresenters;

        public ResourceCardPresenter GetPresenter(Resource resource)
        {
            ResourceCardPresenter presenter = null;
            int index = _resourceCardPresenters.FindIndex(p => p.Resource.Equals(resource));
            if (index > -1)
            {
                presenter = _resourceCardPresenters[index];
            }

            return presenter;
        }

        protected void OnEnable()
        {
            InitDeck();
            UpdateDeck();
        }

        private void UpdateDeck()
        {
            List<Resource> currentDeck = _resourceManager.CurrentDeck;
            for (int i = 0; i < currentDeck.Count; i++)
            {
                Resource resource = currentDeck[i];
                if (_cardParents[i].childCount == 0)
                {
                    ResourceCardPresenter presenter = GetPresenter(resource);
                    presenter.transform.SetParent(_cardParents[i]);
                    presenter.transform.localPosition = Vector3.zero;
                    presenter.Flip();

                    _currentDeckPresenters.Add(presenter);
                }
            }
        }

        private void InitDeck()
        {
            List<Resource> resources = _resourceManager.Resources;
            for (int i = 0; i < resources.Count; i++)
            {
                Resource resource = resources[i];
                ResourceCardItem item = GetResourceCardItem(resource);

                ResourceCardPresenter presenter = _instantiator.InstantiatePrefab<ResourceCardPresenter>(item.Presenter, _flippedDeckParent);
                _resourceCardPresenters.Add(presenter);

                presenter.Resource = resource;
                presenter.transform.Translate(_nextCardOffset * i);
                presenter.Flip();
            }
        }

        private ResourceCardItem GetResourceCardItem(Resource resource)
        {
            ResourceCardItem resourceCardItem = null;
            int itemIndex = _resourceCardPrefabs.FindIndex(p => p.Type.Equals(resource.Type));
            if (itemIndex > -1)
            {
                resourceCardItem = _resourceCardPrefabs[itemIndex];
            }
            else
            {
                Debug.LogError($"Can't find ResourceCardItem for {resource.Type}");
            }

            return resourceCardItem;
        }


        [Serializable]
        private class ResourceCardItem
        {
            [SerializeField]
            private ResourceType _resourceType;

            [SerializeField]
            private ResourceCardPresenter _presenter;

            public ResourceType Type => _resourceType;

            public ResourceCardPresenter Presenter => _presenter;
        }
    }
}
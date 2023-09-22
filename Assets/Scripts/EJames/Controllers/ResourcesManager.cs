#region

using System;
using System.Collections.Generic;
using EJames.Models;
using EJames.Utility;

#endregion

namespace EJames.Controllers
{
    public class ResourcesManager : IInitable
    {
        private int _deckOffset;

        private List<Resource> _resources = new List<Resource>();
        private List<Resource> _currentDeck = new List<Resource>();
        private const int _deckSize = 9;

        public event Action DeckUpdated;

        public List<Resource> Resources => _resources;

        public List<Resource> CurrentDeck => _currentDeck;

        void IInitable.Init()
        {
            _deckOffset = 0;

            AddResources(ResourceType.Slave, 18);

            AddResources(ResourceType.Cooper, 6);
            AddResources(ResourceType.Silver, 6);
            AddResources(ResourceType.Gold, 6);
            AddResources(ResourceType.Documents, 6);
            AddResources(ResourceType.Food, 6);
            AddResources(ResourceType.Fish, 6);

            _resources.Shuffle();

            AddCardsToDeck();
        }

        private void AddResources(ResourceType type, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                _resources.Add(new Resource(type));
            }
        }

        private void AddCardsToDeck()
        {
            bool wasUpdated = false;
            while (_resources.Count > _deckOffset && _currentDeck.Count < _deckSize)
            {
                _currentDeck.Add(_resources[_deckOffset++]);
                wasUpdated = true;
            }

            if (wasUpdated)
            {
                DeckUpdated?.Invoke();
            }
        }
    }
}
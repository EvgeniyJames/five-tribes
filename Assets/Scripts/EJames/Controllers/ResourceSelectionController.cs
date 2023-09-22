using System;
using System.Collections.Generic;
using EJames.Models;

namespace EJames.Controllers
{
    public class ResourceSelectionController
    {
        private List<Resource> _selectedResources = new List<Resource>();

        private int _amount;

        public bool IsActive { get; set; }

        public event Action<Resource> Selected;
        public event Action<Resource> Deselected;

        public event Action Done;

        public void SelectResources(int amount)
        {
            _amount = amount;
            IsActive = true;
        }

        public void ProcessedResource(Resource resource)
        {
            if (IsActive)
            {
                if (_selectedResources.Contains(resource))
                {
                    OnResourceDeselect(resource);
                }
                else
                {
                    OnResourceSelect(resource);
                }
            }
        }

        private void OnResourceSelect(Resource resource)
        {
            if (_selectedResources.Count < _amount)
            {
                _selectedResources.Add(resource);
                Selected?.Invoke(resource);
            }
        }

        private void OnResourceDeselect(Resource resource)
        {
            _selectedResources.Remove(resource);
            Deselected?.Invoke(resource);
        }

        public void OnDone()
        {
            foreach(Resource resource in _selectedResources)
            {
                Deselected?.Invoke(resource);
            }

            _selectedResources.Clear();

            IsActive = false;
            Done?.Invoke();
        }
    }
}
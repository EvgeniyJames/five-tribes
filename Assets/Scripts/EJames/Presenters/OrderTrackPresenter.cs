#region

using System.Collections.Generic;
using EJames.Controllers;
using EJames.Models;
using EJames.Views;
using UnityEngine;
using Zenject;

#endregion

namespace EJames.Presenters
{
    public class OrderTrackPresenter : MonoBehaviour
    {
        [SerializeField]
        private List<OrderSlotView> _orderViews;

        [Inject]
        private PlayerOrderController _playerOrderController;

        protected void Awake()
        {
            for (int i = 0; i < _playerOrderController.OrderSlots.Count; i++)
            {
                OrderSlot orderSlot = _playerOrderController.OrderSlots[i];
                _orderViews[i].Init(orderSlot);
            }
        }
    }
}
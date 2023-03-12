#region

using System;
using System.Collections.Generic;
using EJames.Models;

#endregion

namespace EJames.Controllers
{
    public class PlayerOrderController
    {
        private List<OrderSlot> _orderSlots = new List<OrderSlot>
        {
            new OrderSlot
            {
                Order = 1,
            },
            new OrderSlot
            {
                Order = 2,
            },
            new OrderSlot
            {
                Order = 3,
            },
            new OrderSlot
            {
                Order = 4,
            },
        };

        public event Action OrderChanged;

        public List<OrderSlot> OrderSlots => _orderSlots;

        public void SetPlayerOrder(Player player, int order)
        {
            _orderSlots[order].Player = player;
            OrderChanged?.Invoke();
        }
    }
}
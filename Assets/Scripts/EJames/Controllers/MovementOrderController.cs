using System.Collections.Generic;
using EJames.Models;

namespace EJames.Controllers
{
    public class MovementOrderController
    {
        public List<MovementOrderSlot> MovementOrderSlots { get; } = new List<MovementOrderSlot>();
    }
}
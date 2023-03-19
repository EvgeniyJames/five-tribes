#region

using System;
using System.Collections.Generic;
using EJames.Models;

#endregion

namespace EJames.Controllers
{
    public class PlayerHandController
    {
        public event Action MeeplesUpdated;

        public List<Meeple> Meeples { get; } = new List<Meeple>();

        public void SetMeeples(List<Meeple> meeples)
        {
            Meeples.Clear();
            foreach (Meeple meeple in meeples)
            {
                Meeples.Add(meeple);
            }

            MeeplesUpdated?.Invoke();
        }
    }
}
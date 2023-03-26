#region

using System.Collections.Generic;

#endregion

namespace EJames.Models
{
    public class MeepleBag
    {
        private List<Meeple> _meeples = new List<Meeple>();

        public event MeeplePlacedDelegate MeeplePlaced;

        public void PlaceMeeple(Meeple meeple)
        {
            _meeples.Add(meeple);
            MeeplePlaced?.Invoke(meeple);
        }

        public delegate void MeeplePlacedDelegate(Meeple meeple);
    }
}
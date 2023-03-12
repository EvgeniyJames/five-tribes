#region

using System;

#endregion

namespace EJames.Models
{
    public class Player
    {
        private int _color;

        public event Action ColorChanged;

        public long Id { get; set; }

        public int Color
        {
            get => _color;
            set
            {
                if (_color != value)
                {
                    _color = value;
                    ColorChanged?.Invoke();
                }
            }
        }
    }
}
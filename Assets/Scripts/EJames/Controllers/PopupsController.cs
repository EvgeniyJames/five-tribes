#region

using System.Collections.Generic;
using EJames.Popups;
using EJames.Utility;

#endregion

namespace EJames.Controllers
{
    public class PopupsController : IInitable
    {
        private List<IPopup> _popups = new List<IPopup>();

        public void RegisterPopup(IPopup popup)
        {
            _popups.Add(popup);
        }

        public void ShowPopup<T>(Dictionary<string, object> args = null) where T : IPopup
        {
            int popupIndex = _popups.FindIndex(p => p.GetType() == typeof(T));
            if (popupIndex > -1)
            {
                _popups[popupIndex].Show(args);
            }
        }

        public void HidePopup<T>() where T : IPopup
        {
            int popupIndex = _popups.FindIndex(p => p.GetType() == typeof(T));
            if (popupIndex > -1)
            {
                _popups[popupIndex].Hide();
            }
        }

        public T GetPopup<T>() where T : class, IPopup
        {
            IPopup popup = null;
            int popupIndex = _popups.FindIndex(p => p.GetType() == typeof(T));
            if (popupIndex > -1)
            {
                popup = _popups[popupIndex];
            }

            return popup as T;
        }

        void IInitable.Init()
        {
        }
    }
}
using System.Collections.Generic;

namespace EJames.Popups
{
    public interface IPopup
    {
        void Show(Dictionary<string, object> args = null);
        void Hide();
    }
}
#region

using System.Collections.Generic;

#endregion

namespace EJames.Popups
{
    public interface IPopup
    {
        void Show(Dictionary<string, object> args = null);
        void Hide();
    }
}
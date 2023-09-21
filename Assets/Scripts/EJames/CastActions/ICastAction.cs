#region

using EJames.Controllers;
using EJames.Models;

#endregion

namespace EJames.CastActions
{
    public interface ICastAction
    {
        Meeple.MeepleType Type { get; }

        void DoAction(CastActionController.Args args);
    }
}
#region

using System.Collections.Generic;
using EJames.CastActions;
using EJames.Models;
using EJames.Utility;
using UnityEngine;
using Zenject;

#endregion

namespace EJames.Controllers
{
    public class CastActionController : IInitable
    {
        [Inject]
        private Instantiator _instantiator;

        private List<ICastAction> _castActions = new List<ICastAction>();

        public void ProcessCastAction(Args args)
        {
            if (args.LastMeeples.Count > 0)
            {
                Meeple lastMeeple = args.LastMeeples[0];
                Meeple.MeepleType lastMeepleType = lastMeeple.Type;
                ICastAction castAction = GetCastAction(lastMeepleType);
                castAction?.DoAction(args);
            }
        }

        void IInitable.Init()
        {
            _castActions.Add(_instantiator.Instantiate<VizierCastAction>());
            _castActions.Add(_instantiator.Instantiate<ElderCastAction>());
        }

        private ICastAction GetCastAction(Meeple.MeepleType meepleType)
        {
            ICastAction castAction = null;
            int actionIndex = _castActions.FindIndex(ca => ca.Type.Equals(meepleType));
            if (actionIndex > -1)
            {
                castAction = _castActions[actionIndex];
            }
            else
            {
                Debug.LogError($"Can't find Cast action for {meepleType}");
            }

            return castAction;
        }

        public class Args
        {
            public Args(List<Meeple> lastMeeples, Cell lastCell, Player player)
            {
                LastMeeples = lastMeeples;
                LastCell = lastCell;
                Player = player;
            }

            public List<Meeple> LastMeeples { get; }

            public Cell LastCell { get; }

            public Player Player { get; }
        }
    }
}
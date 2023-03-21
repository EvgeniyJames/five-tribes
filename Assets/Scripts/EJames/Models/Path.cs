#region

using System.Collections.Generic;

#endregion

namespace EJames.Models
{
    public class Path
    {
        public List<PathNode> PathNodes { get; } = new List<PathNode>();
    }
}
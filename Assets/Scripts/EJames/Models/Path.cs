#region

using System.Collections.Generic;

#endregion

namespace EJames.Models
{
    public class Path
    {
        public List<PathNode> PathNodes { get; } = new List<PathNode>();

        public override bool Equals(object obj)
        {
            if (obj is not Path other)
            {
                return false;
            }

            if (PathNodes.Count != other.PathNodes.Count)
            {
                return false;
            }

            for (int i = 0; i < PathNodes.Count; i++)
            {
                PathNode pathNode = PathNodes[i];
                PathNode pathNodeOther = other.PathNodes[i];
                if (!pathNode.Equals(pathNodeOther))
                {
                    return false;
                }
            }

            return false;
        }

        public bool EqualsDeep(Path other)
        {
            if (PathNodes.Count < other.PathNodes.Count)
            {
                return false;
            }

            for (int i = 0; i < other.PathNodes.Count; i++)
            {
                PathNode otherPathNode = other.PathNodes[i];
                PathNode pathNode = PathNodes[i];

                if (!pathNode.Equals(otherPathNode))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
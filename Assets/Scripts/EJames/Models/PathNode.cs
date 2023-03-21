namespace EJames.Models
{
    public class PathNode
    {
        public PathNode(Cell cell, Meeple meepleLeft)
        {
            Cell = cell;
            MeepleLeft = meepleLeft;
        }

        public Cell Cell { get; }

        public Meeple MeepleLeft { get; }
    }
}
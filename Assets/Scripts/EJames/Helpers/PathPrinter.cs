#region

using System.Text;
using EJames.Models;
using UnityEngine;

#endregion

namespace EJames.Helpers
{
    public static class PathPrinter
    {
        public static void PrintMovement(Movement movement)
        {
            StringBuilder pathString = new StringBuilder();
            pathString.Append($"{movement.StartCell}:");

            foreach (PathNode pathNode in movement.Path.PathNodes)
            {
                pathString.Append($" -> {pathNode.Cell} ({pathNode.MeepleLeft.Type.ToString()})");
            }

            Debug.Log($"Path: {pathString}");
        }
    }
}
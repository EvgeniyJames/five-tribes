﻿#region

using System;
using System.Collections.Generic;

#endregion

namespace EJames.Utility
{
    public static class ListExtensions
    {
        private static Random _random = new Random();

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = _random.Next(n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }
    }
}
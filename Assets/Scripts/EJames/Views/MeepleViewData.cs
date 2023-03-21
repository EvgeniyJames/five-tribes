#region

using System;
using EJames.Models;
using UnityEngine;

#endregion

namespace EJames.Views
{
    [Serializable]
    public class MeepleViewData
    {
        [SerializeField]
        private Meeple.MeepleType _meepleType;

        [SerializeField]
        private GameObject _go;

        public Meeple.MeepleType MeepleType => _meepleType;

        public GameObject GO => _go;
    }
}
#region

using System;
using System.Collections.Generic;
using EJames.Controllers;
using EJames.Utility;
using UnityEngine;
using Zenject;

#endregion

namespace EJames.Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField]
        private PlayerFieldPresentersController _playerFieldPresentersController;

        private List<Type> _initablesTypes = new List<Type>
        {
            typeof(PopupsController),
            typeof(PlayersController),
            typeof(ColorsController),
            typeof(PlayersAuctionController),
            typeof(PlayerOrderController),
            typeof(GameStateController),
        };

        public override void InstallBindings()
        {
            Instantiator instantiator = new Instantiator(Container);
            Container.Bind<Instantiator>().FromInstance(instantiator).AsSingle();

            foreach (Type type in _initablesTypes)
            {
                object initableObject = Container.Instantiate(type);
                Container.Bind(type).FromInstance(initableObject).AsSingle();

                if (initableObject is IInitable initable)
                {
                    initable.Init();
                }
            }

            Container.Bind<PlayerFieldPresentersController>().FromInstance(_playerFieldPresentersController).AsSingle();
        }
    }
}
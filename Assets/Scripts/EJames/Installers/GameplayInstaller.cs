﻿#region

using System;
using System.Collections.Generic;
using EJames.Controllers;
using EJames.Presenters;
using EJames.Utility;
using UnityEngine;
using Zenject;

#endregion

namespace EJames.Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField]
        private PlayerFieldPresenters _playerFieldPresenters;

        [SerializeField]
        private GridPresenter _gridPresenter;

        [SerializeField]
        private PlayerAuctionPresenter _playerAuctionPresenter;

        [SerializeField]
        private ResourcesDeckPresenter _resourcesDeckPresenter;

        private List<Type> _initablesTypes = new List<Type>
        {
            typeof(ResourceSelectionController),
            typeof(ResourcesManager),
            typeof(PlayerResourcesesController),
            typeof(PlayerMeeplesController),
            typeof(CastActionController),
            typeof(PlayerCellsController),
            typeof(MeepleBagController),
            typeof(PopupsController),
            typeof(PlayersController),
            typeof(PlayerSequenceController),
            typeof(ColorsController),
            typeof(PlayersAuctionController),
            typeof(PlayerOrderController),
            typeof(GridController),
            typeof(CellSelectController),
            typeof(PossibleMovementController),
            typeof(PlayerHandController),
            typeof(PlayerMovementController),
            typeof(GameTestController),
            typeof(PlayerMovementFinishController),

            //Must be last
            typeof(GameStateController),
        };

        private List<IInitable> _initables = new List<IInitable>();

        public override void InstallBindings()
        {
            Instantiator instantiator = new Instantiator(Container);
            Container.Bind<Instantiator>().FromInstance(instantiator).AsSingle();

            Container.Bind<PlayerFieldPresenters>().FromInstance(_playerFieldPresenters).AsSingle();
            Container.Bind<GridPresenter>().FromInstance(_gridPresenter).AsSingle();
            Container.Bind<PlayerAuctionPresenter>().FromInstance(_playerAuctionPresenter).AsSingle();
            Container.Bind<ResourcesDeckPresenter>().FromInstance(_resourcesDeckPresenter).AsSingle();

            foreach (Type type in _initablesTypes)
            {
                object initableObject = Container.Instantiate(type);
                Container.Bind(type).FromInstance(initableObject).AsSingle();

                if (initableObject is IInitable initable)
                {
                    _initables.Add(initable);
                }
            }

            foreach (IInitable initable in _initables)
            {
                initable.Init();
            }
        }
    }
}
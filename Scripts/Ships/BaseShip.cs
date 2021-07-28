using System.Collections.Generic;
using System.Data.Common;
using BlueKnightOne.Ships.ShipSystems;
using Godot;

namespace BlueKnightOne.Ships
{
    public abstract class BaseShip : Node 
    {
        // List of ship systems
        private readonly List<BaseShipSystem> shipSystems = new List<BaseShipSystem>();
    }
}